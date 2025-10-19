using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RabbitMQCompanyListener.Domain.Entities;

namespace RabbitMQCompanyListener.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<Company> Companys { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
