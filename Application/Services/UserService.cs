using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Application.Configuration;
using Application.Exceptions;
using Application.Extensions;
using Application.Interfaces;
using Application.Models.REST.Request;
using Application.Models.REST.Response;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace Application.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IGlobalConstants _globalConstants;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IRolesService _rolesService;
    private AuthOptions _authOptions;

    public UserService(
        UserManager<ApplicationUser> userManager,
        IGlobalConstants globalConstants,
        IUnitOfWork unitOfWork,
        SignInManager<ApplicationUser> signInManager,
        IMapper mapper,
        IOptionsMonitor<AuthOptions> authOptions,
        IRolesService rolesService)
    {
        _userManager = userManager;
        _globalConstants = globalConstants;
        _signInManager = signInManager;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _authOptions = authOptions.CurrentValue;
        _rolesService = rolesService;
    }
    
    public async Task<UserResponse> Create(UserRequest userRequest)
    {
        var applicationUser = _mapper.Map<ApplicationUser>(userRequest);

        var identityResult = await _userManager.CreateAsync(applicationUser, userRequest.Password);

        if (!identityResult.Succeeded)
        {
            throw new UserCreationException("Failed to create user",
                identityResult.Errors.Select(e => e.Description).ToList());
        }

        await _unitOfWork.CompleteAsync();
        return _mapper.Map<UserResponse>(applicationUser);
    }

    public async Task<AuthenticateResponse> Login(AuthenticateRequest authenticateRequest)
    {
        var signInResult = await _signInManager.PasswordSignInAsync(
            authenticateRequest.Username,
            authenticateRequest.Password,
            false,
            false);

        if (signInResult == null || signInResult.Succeeded == false)
            throw new AuthenticationException("Username or password is incorrect");

        var applicationUser = await _unitOfWork.UserRepository.QueryInclUserRoles(authenticateRequest.Username);

        if (applicationUser == null)
            throw new ApplicationException("Internal user record not found");

        var authenticationResponse = _mapper.Map<AuthenticateResponse>(applicationUser);
        authenticationResponse.Token = GenerateJsonWebToken(applicationUser);

        return authenticationResponse;
    }

    public async Task<UserResponse> Get(Guid userId)
    {
        var result = await _unitOfWork.UserRepository.QueryInclUserRoles(userId);

        if (result == null)
            throw new NotFoundException($"User not found for Id: {userId}");

        return _mapper.Map<UserResponse>(result);
    }

    public async Task Delete(Guid userId)
    {
        var userToDelete = await _unitOfWork.UserRepository.Query(userId);

        if (userToDelete == null)
            throw new NotFoundException($"No user was found with the id {userId}");
        
        _unitOfWork.UserRepository.Delete(userToDelete);
        await _unitOfWork.CompleteAsync();
    }

    public async Task<UserResponse> UpdateProperties(Guid userId, JsonPatchDocument<UserRequest> patchDocument)
    {
        var existingUser = await _unitOfWork.UserRepository.QueryInclUserRoles(userId);

        if (existingUser == null)
            throw new NotFoundException($"No user was found with the Id: {userId}");

        var userRequest = _mapper.Map<UserRequest>(existingUser);
        var passwordChangeRequest = patchDocument.Operations.FirstOrDefault(o
            => o.path.Equals("/password", StringComparison.CurrentCultureIgnoreCase));

        if (passwordChangeRequest != null)
            await UpdatePassword(patchDocument, existingUser, passwordChangeRequest);
        
        var roleChangeRequest = patchDocument.Operations.FirstOrDefault(o
            => o.path.Equals("/roles", StringComparison.CurrentCultureIgnoreCase));

        if (roleChangeRequest != null)
            await UpdateRoles(patchDocument, existingUser, roleChangeRequest);
        
        JsonPatchDocumentExtensions.CheckAttributesThenApply(patchDocument, userRequest, 
            error => throw new JsonPatchException(error), _globalConstants.CurrentUserRoles);
        
        _mapper.Map(userRequest, existingUser);

        await _unitOfWork.CompleteAsync();

        return _mapper.Map<UserResponse>(existingUser);
    }

    private string GenerateJsonWebToken(ApplicationUser applicationUser)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_authOptions.Secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, applicationUser.Id.ToString()),
            new(ClaimTypes.Email, applicationUser.Email)
        };

        foreach (var role in applicationUser.UserRoles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role.Role.Name));
        }

        var token = new JwtSecurityToken(
            "AthenaToken",
            "AthenaToken",
            claims: claims,
            expires: DateTime.Now.AddHours(2),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private async Task UpdatePassword(JsonPatchDocument<UserRequest> patchDocument, ApplicationUser existingUser,
        Operation<UserRequest> passwordChangeRequest)
    {
        await _userManager.SetLockoutEnabledAsync(existingUser, false);
        await _userManager.RemovePasswordAsync(existingUser);
        await _userManager.AddPasswordAsync(existingUser, passwordChangeRequest.value.ToString());
        patchDocument.Operations.Remove(passwordChangeRequest);
    }

    private async Task UpdateRoles(JsonPatchDocument<UserRequest> patchDocument, ApplicationUser existingUser,
        Operation<UserRequest> roleChangeRequest)
    {
        var currentUserRoles = existingUser.UserRoles.Select(ur => ur.Role.Name).ToList();
        if (currentUserRoles.Any()) await _userManager.RemoveFromRolesAsync(existingUser, currentUserRoles);
        
        var updatedRoles = roleChangeRequest.value is List<string> ? 
            (List<string>)roleChangeRequest.value : ((JArray) roleChangeRequest.value).ToObject<List<string>>();

        if (updatedRoles != null && updatedRoles.Any())
        {
            await _userManager.AddToRolesAsync(existingUser, updatedRoles);
        }

        patchDocument.Operations.Remove(roleChangeRequest);
    }
}