using Application.Models.REST.Request;
using Application.Models.REST.Response;
using AutoMapper;
using Core.Entities;

namespace Application.Mappings;

public class PostMappings : Profile
{
    public PostMappings()
    {
        CreatePostMappings();
    }

    private void CreatePostMappings()
    {
        CreateMap<PostRequest, Post>().ReverseMap();
        CreateMap<Post, PostResponse>();
    }
}