using Application.Exceptions;
using Application.Extensions;
using Application.Interfaces;
using Application.Models.REST.Request;
using Application.Models.REST.Response;
using Athena.Core.Interfaces;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;

namespace Application.Services;

public class RoleService : IRolesService
{
    private readonly IGlobalConstants _constants;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public RoleService(IGlobalConstants constants,
        IMapper mapper,
        IUnitOfWork unitOfWork)
    {
        _constants = constants;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }
    
    public async Task<RoleResponse> Get(Guid roleId)
    {
        var result = await _unitOfWork.RolesRepository.Query(roleId);
        if (result == null)
            throw new NotFoundException($"Role not found for Id: {roleId}");
         
        return _mapper.Map<RoleResponse>(result);
    }

    public async Task<RoleResponse> Create(ApplicationRoleRequest applicationRoleRequest)
    {
        var newUserRole = _mapper.Map<ApplicationRole>(applicationRoleRequest);
        await _unitOfWork.RolesRepository.AddAsync(newUserRole);
        await _unitOfWork.CompleteAsync();

        return _mapper.Map<RoleResponse>(newUserRole);
    }

    public async Task<RoleResponse> UpdateProperties(Guid roleId, JsonPatchDocument<ApplicationRoleRequest> patchDocument)
    {
        var existingRole = await _unitOfWork.RolesRepository.Query(roleId);
        if (existingRole == null)
            throw new NotFoundException($"Role was not found with the ID: {roleId}");

        var roleRequest = _mapper.Map<ApplicationRoleRequest>(existingRole);
        
        JsonPatchDocumentExtensions.CheckAttributesThenApply(patchDocument, roleRequest, 
            error => throw new JsonPatchException(error), _constants.CurrentUserRoles);

        _mapper.Map(roleRequest, existingRole);

        await _unitOfWork.CompleteAsync();

        return _mapper.Map<RoleResponse>(existingRole);
    }
}