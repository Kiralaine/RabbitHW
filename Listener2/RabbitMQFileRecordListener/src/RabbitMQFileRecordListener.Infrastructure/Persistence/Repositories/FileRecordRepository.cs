using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQFileRecordListener.Application.Interfaces;
using RabbitMQFileRecordListener.Domain.Entities;

namespace RabbitMQFileRecordListener.Infrastructure.Persistence.Repositories;

public class FileRecordRepository : IFileRecordRepository
{
    private readonly AppDbContext _context;

    public FileRecordRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(FileRecord fileRecord)
    {
        _context.FileRecords.Add(fileRecord);
        await _context.SaveChangesAsync();
    }

}
