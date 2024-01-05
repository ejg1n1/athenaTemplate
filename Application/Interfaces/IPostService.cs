using Application.Models.REST.Request;
using Application.Models.REST.Response;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Interfaces;

public interface IPostService
{
    Task<PostResponse> Get(Guid postId);
    Task<PostResponse> Create(PostRequest postRequest);
    Task<PostResponse> UpdateProperties(Guid postId, JsonPatchDocument<PostRequest> patchDocument);
}