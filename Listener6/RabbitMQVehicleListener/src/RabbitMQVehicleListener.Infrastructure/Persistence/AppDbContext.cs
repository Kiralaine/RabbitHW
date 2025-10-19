using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RabbitMQVehicleListener.Domain.Entities;

namespace RabbitMQVehicleListener.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Vehicle> Vehicles { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
