using RabbitMQFileRecordBuilder.Application.DTO_s;
using RabbitMQFileRecordBuilder.Domain.Entities;

namespace RabbitMQFileRecordBuilder.Application.Interfaces;

public interface IFileRecordRepository
{
    Task<FileRecord> AddAsync(FileRecord file);
}
