using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repository;

public class RepositoryContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; init; }
    public DbSet<Courier> Couriers { get; init; }
    public DbSet<Review> Reviews { get; init; }
    public DbSet<Order> Orders { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Order>().Property(o => o.OrderStatus).HasConversion<string>();
    }
}