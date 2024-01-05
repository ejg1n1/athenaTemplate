using Application.Models.REST.Request;
using Application.Models.REST.Response;
using AutoMapper;
using Core.Entities;

namespace Application.Mappings;

public class RoleMappings : Profile
{
    public RoleMappings()
    {
        CreateRoleMappings();
    }

    private void CreateRoleMappings()
    {
        CreateMap<ApplicationRole, RoleResponse>()
            .ForMember(d => d.Name, o
                => o.MapFrom(s => s.Name));

        CreateMap<ApplicationRoleRequest, ApplicationRole>().ReverseMap();
    }
}