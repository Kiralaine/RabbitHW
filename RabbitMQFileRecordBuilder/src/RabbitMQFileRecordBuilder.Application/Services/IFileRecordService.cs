using RabbitMQFileRecordBuilder.Application.DTO_s;
using RabbitMQFileRecordBuilder.Domain.Entities;

namespace RabbitMQFileRecordBuilder.Application.Services;

public interface IFileRecordService
{
    Task<FileRecord> AddAsync(FileRecordDto dto);
}