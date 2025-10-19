using RabbitMQFileRecordListener.Domain.Entities;

namespace RabbitMQFileRecordListener.Application.Interfaces;

public interface IFileRecordRepository
{
    Task AddAsync(FileRecord fileRecord);
}