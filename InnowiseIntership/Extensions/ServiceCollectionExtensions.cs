using InnowiseIntership.Auth0;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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


    public static void AddCertificateAndAuth0(this IServiceCollection collection,IConfiguration configuration)
    {

        var certificate = new Certificate(configuration
            .GetSection("Certificate")
            .GetSection("path").Value!);

        var domain = $"https://{configuration.GetSection("Auth0").GetSection("Domain").Value}/";
        var audience = configuration.GetSection("Auth0").GetSection("Audience").Value;
        
        collection.AddSingleton<ICertificate>(certificate);
        collection.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidateIssuer = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = audience,
                        ValidIssuer = domain,
                        IssuerSigningKey = certificate.PublicKey
                    };
                }
            );

        collection.AddAuthorization(options =>
        {
            options.AddPolicy("CRUD:users",
                policy => policy.Requirements.Add(
                    new HasScopeRequirement("CRUD:users",domain)));
        });
    }
    
}