using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RabbitMQCompanyListener.Domain.Entities;

namespace RabbitMQCompanyListener.Infrastructure.Persistence.Configurations;

public class CompanyConfiguration : IEntityTypeConfiguration<Company>
{
    public void Configure(EntityTypeBuilder<Company> builder)
    {
        builder.ToTable("Companys");

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
