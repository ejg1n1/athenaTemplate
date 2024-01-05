using Application.Exceptions;
using Application.Interfaces;
using Application.Models.REST.Request;
using Application.Models.REST.Response;
using AutoMapper;
using Core.Interfaces;
using Microsoft.AspNetCore.JsonPatch;

namespace Application.Services;

public class PostService : IPostService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    
    public PostService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<PostResponse> Get(Guid postId)
    {
        var result = await _unitOfWork.PostRepository.Query(postId);
        if (result == null)
            throw new NotFoundException($"Post not found for Id: {postId}");

        return _mapper.Map<PostResponse>(result);
    }

    public Task<PostResponse> Create(PostRequest postRequest)
    {
        throw new NotImplementedException();
    }

    public Task<PostResponse> UpdateProperties(Guid postId, JsonPatchDocument<PostRequest> patchDocument)
    {
        throw new NotImplementedException();
    }
}