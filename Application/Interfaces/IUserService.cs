using Application.Models.REST.Request;
using Application.Models.REST.Response;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Interfaces;

public interface IUserService
{
    Task<UserResponse> Create(UserRequest userRequest);

    Task<AuthenticateResponse> Login(AuthenticateRequest authenticateRequest);
    
    Task<UserResponse> Get(Guid userId);
    
    Task Delete(Guid userId);
    
    Task<UserResponse> UpdateProperties(Guid userId, JsonPatchDocument<UserRequest> patchDocument);
}