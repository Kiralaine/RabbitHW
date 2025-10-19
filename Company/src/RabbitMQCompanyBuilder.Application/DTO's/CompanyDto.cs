namespace RabbitMQCompanyBuilder.Application.DTO_s;

public class CompanyDto
{
    public string FileName { get; set; } = string.Empty;
    public string FilePath { get; set; } = string.Empty;
    public long FileSize { get; set; }
    public string ContentType { get; set; } = string.Empty;
    public string UploadedBy { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public bool IsPublic { get; set; }
    public string Hash { get; set; } = string.Empty;
}
