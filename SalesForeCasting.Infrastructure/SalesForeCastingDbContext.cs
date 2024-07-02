using Microsoft.EntityFrameworkCore;
using SalesForeCasting.Infrastructure.Entities;

namespace SalesForeCasting.Infrastructure;

public class SalesForeCastingDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("User ID=postgres;Password=local-only-pwd;Host=localhost;Port=5432;Database=SalesForeCasting;");
    }

    public DbSet<Order> Orders { get; set; }

    public DbSet<Product> Products { get; set; }

    public DbSet<Return> Returns { get; set; }
}