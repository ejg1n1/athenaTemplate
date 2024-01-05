using Application.Interfaces;
using Application.Services;
using Core.Interfaces;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace infra.ioc;

public static class InfrastructureServiceExtensions
{
    public static IServiceCollection AddDbContextServices(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        
        serviceCollection.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseNpgsql(configuration.GetConnectionString("ApplicationDatabase"));
        });
        return serviceCollection;
    }

    public static IServiceCollection AddDependencyInjectionServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
        serviceCollection.AddScoped<IAddressStatusService, AddressStatusService>();
        serviceCollection.AddScoped<IPostStatusService, PostStatusService>();
        return serviceCollection;
    }
}