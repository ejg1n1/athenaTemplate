using Application.Interfaces;
using Application.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Ioc;

public static class DependencyContainer
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddLazyCache();

        services.AddScoped<IRolesService, RoleService>();
        services.AddScoped<IGlobalConstants, GlobalConstantsService>();
        services.AddScoped<IUserService, UserService>();
        return services;
    }
}