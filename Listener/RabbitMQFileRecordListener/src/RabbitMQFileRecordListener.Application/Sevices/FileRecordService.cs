using RabbitMQFileRecordListener.Application.Interfaces;
using RabbitMQFileRecordListener.Domain.Entities;

namespace RabbitMQFileRecordListener.Application.Sevices;

public class FileRecordService : IFileRecordService
{
    private readonly IFileRecordRepository _repository;

    public FileRecordService(IFileRecordRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(FileRecord fileRecord)
    {
        await _repository.AddAsync(fileRecord);
    }
}
