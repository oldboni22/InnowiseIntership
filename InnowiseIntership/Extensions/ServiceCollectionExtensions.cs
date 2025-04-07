using Auth0.AspNetCore.Authentication;
using InnowiseIntership.Auth0;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Contracts;
using Service;
using Service.Contracts;

namespace InnowiseIntership.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection collection,IConfiguration configuration)
    {
        collection.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration
                .GetConnectionString("sqlConnection")));
    }

    public static void AddRepositoryContext(this IServiceCollection collection,IConfiguration configuration)
    {
        collection.AddDbContext<RepositoryContext>(options =>
        {
            options.UseSqlServer(configuration.GetConnectionString("sqlConnection"));
        });
    }
    
    public static void AddRepositoryManager(this IServiceCollection collection)
    {
        collection.AddScoped<IRepositoryManager, RepositoryManager>();
    }

    public static void AddServiceManager(this IServiceCollection collection)
    {
        collection.AddScoped<IServiceManager, ServiceManager>();
    }

    public static void AddAuth0(this IServiceCollection collection,IConfiguration configuration)
    {
        var certificate = new Certificate(configuration
            .GetSection("Certificate")
            .GetSection("path").Value!);
        
        collection.AddSingleton<ICertificate>(certificate);
        
        var domain = $"https://{configuration.GetSection("Auth0").GetSection("Domain").Value}/";
        var clientId = configuration.GetSection("Auth0").GetSection("ClientId").Value;
        var audience = configuration.GetSection("Auth0").GetSection("Audience").Value;
        
        collection.AddAuth0WebAppAuthentication(options =>
        {
            options.Domain = domain;
            options.ClientId = clientId;
            options.ClientAssertionSecurityKey = certificate.Rsa;
        }).WithAccessToken(options =>
        {
            options.Audience = audience;
        });

        collection.AddAuthorizationBuilder()
            .AddPolicy("CRUD:users", policy => policy.Requirements.Add(
                new HasScopeRequirement("CRUD:users", domain)));

        collection.AddSingleton<IAuthorizationHandler, HasScopeHandler>();
    }
    
}