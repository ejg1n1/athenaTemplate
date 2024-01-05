using Application.Exceptions;
using Application.Interfaces;
using Application.Models.REST.Request;
using Application.Models.REST.Response;
using AutoMapper;
using Core.Constants;
using Core.Entities;
using Core.Interfaces;
using LazyCache;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Exceptions;

namespace Application.Services;

public class PostStatusService : IPostStatusService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IAppCache _inMemoryCache;
    
    public PostStatusService(IUnitOfWork unitOfWork, IMapper mapper, IAppCache inMemoryCache)
    {
        _inMemoryCache = inMemoryCache;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }
    
    public async Task<PostStatusResponse> Create(PostStatusRequest postStatusRequest)
    {
        var newPostStatus = _mapper.Map<PostStatus>(postStatusRequest);

        await _unitOfWork.PostStatusRepository.AddAsync(newPostStatus);
        await _unitOfWork.CompleteAsync();
        
        _inMemoryCache.Remove(InMemoryCacheConstants.AllPostStatuses);
        return _mapper.Map<PostStatusResponse>(newPostStatus);
    }

    public async Task<PostStatusResponse> GetPostStatusById(Guid id)
    {
        var allPostStatuses = await GetAllCachedPostStatuses();
        var postStatus = allPostStatuses.FirstOrDefault(a => a.Id == id);

        if (postStatus == null) throw new NotFoundException($"No Post Status found for Id: {id}");

        return _mapper.Map<PostStatusResponse>(postStatus);
    }

    public async Task<PostStatusResponse> GetPostStatusByName(string description)
    {
        var allPostStatuses = await GetAllCachedPostStatuses();
        var postStatus = allPostStatuses.FirstOrDefault(a => a.Description == description);

        if (postStatus == null) throw new NotFoundException($"No Post Status found for Description: {description}");

        return _mapper.Map<PostStatusResponse>(postStatus);
    }

    public async Task<PostStatusResponse> UpdateProperties(Guid postStatusId, JsonPatchDocument<PostStatusRequest> patchDocument)
    {
        var existingPostStatusToUpdate = await _unitOfWork.PostStatusRepository.Query(postStatusId);

        if (existingPostStatusToUpdate == null)
            throw new NotFoundException($"No post status found for Id: {postStatusId}");

        var postStatusRequest = _mapper.Map<PostStatusRequest>(existingPostStatusToUpdate);
        
        patchDocument.ApplyTo(postStatusRequest, error => throw new JsonPatchException(error));

        _mapper.Map(postStatusRequest, existingPostStatusToUpdate);

        await _unitOfWork.CompleteAsync();
        
        _inMemoryCache.Remove(InMemoryCacheConstants.AllPostStatuses);
        return _mapper.Map<PostStatusResponse>(existingPostStatusToUpdate);
    }

    private async Task<List<PostStatus>> GetAllCachedPostStatuses()
    {
        var addressStatuses = await _inMemoryCache
            .GetOrAddAsync(InMemoryCacheConstants.AllAddressStatuses,
                _GetAllPostStatusesFromDb,
                DateTimeOffset.UtcNow.AddDays(1));

        return addressStatuses;
    }

    private async Task<List<PostStatus>> _GetAllPostStatusesFromDb()
    {
        var postStatuses = await _unitOfWork
            .PostStatusRepository
            .QueryAllWithNoTracking();

        return postStatuses;
    }
}