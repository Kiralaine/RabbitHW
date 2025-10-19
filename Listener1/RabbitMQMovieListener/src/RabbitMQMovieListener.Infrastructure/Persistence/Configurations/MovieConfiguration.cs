using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RabbitMQMovieListener.Domain.Entities;

namespace RabbitMQMovieListener.Infrastructure.Persistence.Configurations;

public class MovieConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.ToTable("Movies");

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
