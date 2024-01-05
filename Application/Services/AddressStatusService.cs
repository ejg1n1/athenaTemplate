using System.Data;
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

public class AddressStatusService : IAddressStatusService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _inMemoryCache;
    
    public AddressStatusService(IMapper mapper, IUnitOfWork unitOfWork, IAppCache inMemoryCache)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
        _inMemoryCache = inMemoryCache;
    }
    
    public async Task<AddressStatusResponse> Create(AddressStatusRequest addressStatusRequest)
    {
        var newAddressStatus = _mapper.Map<AddressStatus>(addressStatusRequest);

        await _unitOfWork.AddressStatusRepository.AddAsync(newAddressStatus);
        await _unitOfWork.CompleteAsync();
        
        _inMemoryCache.Remove(InMemoryCacheConstants.AllAddressStatuses);
        return _mapper.Map<AddressStatusResponse>(newAddressStatus);
    }

    public async Task<AddressStatusResponse> GetAddressStatusById(Guid id)
    {
        var allAddressStatuses = await GetAllCachedAddressStatuses();
        var addressStatus = allAddressStatuses.FirstOrDefault(a => a.Id == id);

        if (addressStatus == null) throw new NotFoundException($"No Address Status found for Id: {id}");

        return _mapper.Map<AddressStatusResponse>(addressStatus);
    }

    public async Task<AddressStatusResponse> GetAddressStatusByName(string description)
    {
        var allAddressStatuses = await GetAllCachedAddressStatuses();
        var addressStatus =
            allAddressStatuses.FirstOrDefault(d => d.Description.Equals(description, StringComparison.OrdinalIgnoreCase));

        if (addressStatus == null) throw new NotFoundException($"No Address Status found for Description: {description}");

        return _mapper.Map<AddressStatusResponse>(addressStatus);
    }

    public async Task<AddressStatusResponse> UpdateProperties(Guid addressStatusId, JsonPatchDocument<AddressStatusRequest> patchDocument)
    {
        var existingAddressStatusToUpdate = await _unitOfWork.AddressStatusRepository.Query(addressStatusId);

        if (existingAddressStatusToUpdate == null)
            throw new NotFoundException($"No address status found for id: {addressStatusId}");

        var addressStatusRequest = _mapper.Map<AddressStatusRequest>(existingAddressStatusToUpdate);
        
        patchDocument.ApplyTo(addressStatusRequest, error => throw new JsonPatchException(error));

        _mapper.Map(addressStatusRequest, existingAddressStatusToUpdate);

        await _unitOfWork.CompleteAsync();
        
        _inMemoryCache.Remove(InMemoryCacheConstants.AllAddressStatuses);
        return _mapper.Map<AddressStatusResponse>(existingAddressStatusToUpdate);
    }

    private async Task<List<AddressStatus>> GetAllCachedAddressStatuses()
    {
        var addressStatuses = await _inMemoryCache
            .GetOrAddAsync(InMemoryCacheConstants.AllAddressStatuses,
                _GetAllAddressStatusesFromDb,
                DateTimeOffset.UtcNow.AddDays(1));

        return addressStatuses;
    }

    private async Task<List<AddressStatus>> _GetAllAddressStatusesFromDb()
    {
        var addressStatuses = await _unitOfWork
            .AddressStatusRepository
            .QueryAllWithNoTracking();

        return addressStatuses;
    }
}