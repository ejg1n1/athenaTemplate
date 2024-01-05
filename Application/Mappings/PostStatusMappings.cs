using Application.Models.REST.Request;
using Application.Models.REST.Response;
using AutoMapper;
using Core.Entities;

namespace Application.Mappings;

public class PostStatusMappings : Profile
{
    public PostStatusMappings()
    {
        CreatePostStatusMappings();
    }

    private void CreatePostStatusMappings()
    {
        CreateMap<PostStatusRequest, PostStatus>().ReverseMap();
        CreateMap<PostStatus, PostStatusResponse>();
    }
}