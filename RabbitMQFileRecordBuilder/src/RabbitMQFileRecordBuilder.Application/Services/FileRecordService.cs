using RabbitMQFileRecordBuilder.Application.DTO_s;
using RabbitMQFileRecordBuilder.Application.Interfaces;
using RabbitMQFileRecordBuilder.Domain.Entities;

namespace RabbitMQFileRecordBuilder.Application.Services;

public class FileRecordService : IFileRecordService
{
    private readonly IFileRecordRepository _fileRecordRepository;

    public FileRecordService(IFileRecordRepository fileRecordRepository)
    {
        _fileRecordRepository = fileRecordRepository;
    }

    public async Task<FileRecord> AddAsync(FileRecordDto dto)
    {
        var fileRecord = new FileRecord
        {
            Id = Guid.NewGuid(),
            FileName = dto.FileName,
            FilePath = dto.FilePath,
            FileSize = dto.FileSize,
            ContentType = dto.ContentType,
            UploadedBy = dto.UploadedBy,
            Description = dto.Description,
            IsPublic = dto.IsPublic,
            Hash = dto.Hash,
            UploadedAt = DateTime.UtcNow
        };

        return await _fileRecordRepository.AddAsync(fileRecord);
    }
}