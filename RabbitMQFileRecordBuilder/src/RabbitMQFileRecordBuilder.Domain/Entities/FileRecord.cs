namespace RabbitMQFileRecordBuilder.Domain.Entities;

public class FileRecord
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FileName { get; set; } = null!;
    public string FilePath { get; set; } = null!;
    public long FileSize { get; set; }
    public string ContentType { get; set; } = null!;
    public DateTime UploadedAt { get; set; }
    public string UploadedBy { get; set; } = null!;
    public string? Description { get; set; }
    public bool IsPublic { get; set; }
    public string Hash { get; set; } = null!;
}
