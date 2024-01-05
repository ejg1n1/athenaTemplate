using Application.Models.REST.Request;
using Application.Models.REST.Response;
using AutoMapper;
using Core.Entities;

namespace Application.Mappings;

public class AddressStatusMappings : Profile
{
    public AddressStatusMappings()
    {
        CreateAddressStatusMappings();
    }

    private void CreateAddressStatusMappings()
    {
        CreateMap<AddressStatusRequest, AddressStatus>().ReverseMap();
        CreateMap<AddressStatus, AddressStatusResponse>();
    }
}