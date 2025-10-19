using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RabbitMQMovieListener.Domain.Entities;

namespace RabbitMQMovieListener.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
