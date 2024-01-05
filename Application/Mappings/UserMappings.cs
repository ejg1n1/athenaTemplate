using Application.Models.REST.Request;
using Application.Models.REST.Response;
using AutoMapper;
using Core.Entities;

namespace Application.Mappings;

public class UserMappings : Profile
{
    public UserMappings()
    {
        CreateUserMappings();
    }

    private void CreateUserMappings()
    {
        CreateMap<UserRequest, ApplicationUser>()
            .ForMember(d => d.FirstName, o 
                => o.MapFrom(s => s.FirstName))
            .ForMember(d => d.LastName, o 
                => o.MapFrom(s => s.LastName))
            .ForMember(d => d.PhoneNumber, o 
                => o.MapFrom(s => s.PhoneNumber))
            .ForMember(d => d.Email, o 
                => o.MapFrom(s => s.EmailAddress))
            .ForMember(d => d.EmailConfirmed, o 
                => o.MapFrom(s => true))
            .ForMember(d => d.SecurityStamp, o 
                => o.MapFrom(s => Guid.NewGuid().ToString()))
            .ForMember(d => d.ConcurrencyStamp, o 
                => o.MapFrom(s => Guid.NewGuid().ToString()))
            .ForMember(d => d.LockoutEnabled, o 
                => o.MapFrom(s => false))
            .ForMember(d => d.UserName, o 
                => o.MapFrom(s => s.EmailAddress))
            .ForMember(d => d.UserRoles, o 
                => o.Ignore())
            .ReverseMap();

        CreateMap<ApplicationUser, AuthenticateResponse>()
            .ForMember(d => d.Roles, o =>
                o.MapFrom(s => s.UserRoles.Select(r => r.Role.Name).ToList()));

        CreateMap<ApplicationUser, UserResponse>()
            .ForMember(d => d.Roles, o
                => o.MapFrom(s => s.UserRoles.Select(r => r.Role.Name).ToList()))
            .ForMember(d => d.EmailAddress, o
                => o.MapFrom(s => s.Email));


    }
}