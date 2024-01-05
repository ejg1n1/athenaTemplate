using Application.Models.REST.Request;
using Application.Models.REST.Response;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Interfaces;

public interface IPostStatusService
{
    Task<PostStatusResponse> Create(PostStatusRequest postStatusRequest);
    Task<PostStatusResponse> GetPostStatusById(Guid id);
    Task<PostStatusResponse> GetPostStatusByName(string description);

    Task<PostStatusResponse> UpdateProperties(Guid postStatusId,
        JsonPatchDocument<PostStatusRequest> patchDocument);
}