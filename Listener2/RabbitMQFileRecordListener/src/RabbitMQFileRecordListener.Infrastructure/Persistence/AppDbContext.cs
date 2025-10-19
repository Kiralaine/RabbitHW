using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RabbitMQFileRecordListener.Domain.Entities;

namespace RabbitMQFileRecordListener.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public DbSet<FileRecord> FileRecords { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
