using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RabbitMQFileRecordListener.Domain.Entities;

namespace RabbitMQFileRecordListener.Infrastructure.Persistence.Configurations;

public class FileRecordConfiguration : IEntityTypeConfiguration<FileRecord>
{
    public void Configure(EntityTypeBuilder<FileRecord> builder)
    {
        builder.ToTable("FileRecords");

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
