using Application.Models.REST.Request;
using Application.Models.REST.Response;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Interfaces;

public interface IRolesService
{
    Task<RoleResponse> Get(Guid roleId);
    Task<RoleResponse> Create(ApplicationRoleRequest applicationRoleRequest);
    Task<RoleResponse> UpdateProperties(Guid roleId, JsonPatchDocument<ApplicationRoleRequest> patchDocument);
}