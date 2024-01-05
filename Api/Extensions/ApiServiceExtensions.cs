using FluentValidation.AspNetCore;
using Microsoft.OpenApi.Models;

namespace Api.Extensions;

public static class ApiServiceExtensions
{
    public static IServiceCollection AddFluentValidationServices(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddFluentValidation(configuration =>
        {
            configuration.RegisterValidatorsFromAssembly(typeof(ApiServiceExtensions).Assembly);
        });
        return serviceCollection;
    }

    public static IServiceCollection AddCorsDevelopmentServices(this IServiceCollection serviceCollection, string developmentCorsPolicy)
    {
        serviceCollection.AddCors(options =>
        {
            options.AddPolicy(developmentCorsPolicy, policy =>
            {
                policy.WithOrigins("http://localhost:3000")
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });
        });
        return serviceCollection;
    }

    public static IServiceCollection AddSwaggerServices(this IServiceCollection serviceCollection)
    {
        var xmlFilename = $"{typeof(ApiServiceExtensions).Assembly.GetName().Name}.xml";
        string xmlFilePath = Path.Combine(AppContext.BaseDirectory, xmlFilename);
        serviceCollection.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Athena API",
                Description = "A .NET 6 API for the Athena solution"
            });
            // options.IncludeXmlComments(xmlFilePath);
            // options.SchemaFilter<ProductSchemaFilter>();
        });
        return serviceCollection;
    }
}