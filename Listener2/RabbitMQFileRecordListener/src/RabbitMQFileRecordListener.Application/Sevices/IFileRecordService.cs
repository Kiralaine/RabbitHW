using RabbitMQFileRecordListener.Domain.Entities;

namespace RabbitMQFileRecordListener.Application.Sevices;

public interface IFileRecordService
{
    Task CreateAsync(FileRecord fileRecord);
}
