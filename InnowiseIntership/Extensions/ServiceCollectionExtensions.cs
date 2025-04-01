using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Contracts;

namespace InnowiseIntership.Extensions;

public static class ServiceCollectionExtensions
{
    public static void ConfigureSqlContext(this IServiceCollection collection,IConfiguration configuration)
    {
        collection.AddDbContext<RepositoryContext>(options =>
            options.UseSqlServer(configuration
                .GetConnectionString("sqlConnection")));
    }
    
    public static void AddRepositoryManager(this IServiceCollection collection)
    {
        collection.AddScoped<IRepositoryManager, RepositoryManager>();
    }
}