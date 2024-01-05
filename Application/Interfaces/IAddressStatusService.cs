using Application.Models.REST.Request;
using Application.Models.REST.Response;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Interfaces;

public interface IAddressStatusService
{
    Task<AddressStatusResponse> Create(AddressStatusRequest addressStatusRequest);
    Task<AddressStatusResponse> GetAddressStatusById(Guid id);
    Task<AddressStatusResponse> GetAddressStatusByName(string name);

    Task<AddressStatusResponse> UpdateProperties(Guid addressStatusId,
        JsonPatchDocument<AddressStatusRequest> patchDocument);

}