using RabbitMQVehicleBuilder.Application.DTO_s;
using RabbitMQVehicleBuilder.Application.Interfaces;
using RabbitMQVehicleBuilder.Domain.Entities;

namespace RabbitMQVehicleBuilder.Application.Services;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _vehicleRepository;

    public VehicleService(IVehicleRepository vehicleRepository)
    {
        _vehicleRepository = vehicleRepository;
    }

    public async Task<Vehicle> AddAsync(VehicleDto dto)
    {
        var vehicle = new Vehicle
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

        return await _vehicleRepository.AddAsync(vehicle);
    }
}