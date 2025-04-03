using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Repository;

namespace InnowiseIntership.ContextFactories;

public class RepositoryContextFactory : IDesignTimeDbContextFactory<RepositoryContext>
{
    public RepositoryContext CreateDbContext(string[] args)
    {
        var cfg = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

        var builder = new DbContextOptionsBuilder()
            .UseSqlServer(cfg.GetConnectionString("sqlConnection")
                ,b => b.MigrationsAssembly("InnowiseIntership"));

        return new RepositoryContext(builder.Options);
    }
}