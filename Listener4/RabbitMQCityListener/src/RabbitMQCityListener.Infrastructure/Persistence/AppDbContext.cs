using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RabbitMQCityListener.Domain.Entities;

namespace RabbitMQCityListener.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<City> Citys { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
