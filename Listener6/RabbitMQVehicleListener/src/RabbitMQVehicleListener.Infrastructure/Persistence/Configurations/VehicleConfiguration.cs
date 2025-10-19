using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RabbitMQVehicleListener.Domain.Entities;

namespace RabbitMQVehicleListener.Infrastructure.Persistence.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.ToTable("Vehicles");

        builder.HasKey(f => f.Id);

        builder.Property(f => f.FileName)
            .IsRequired()
            .HasMaxLength(255);

        builder.Property(f => f.FilePath)
            .IsRequired()
            .HasMaxLength(1024);

        builder.Property(f => f.FileSize)
            .IsRequired();

        builder.Property(f => f.ContentType)
            .HasMaxLength(128);

        builder.Property(f => f.UploadedBy)
            .HasMaxLength(128);

        builder.Property(f => f.Description)
            .HasMaxLength(500);
    }
}
