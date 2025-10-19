using RabbitMQVehicleBuilder.Application.DTO_s;
using RabbitMQVehicleBuilder.Domain.Entities;

namespace RabbitMQVehicleBuilder.Application.Services;

public interface IVehicleService
{
    Task<Vehicle> AddAsync(VehicleDto dto);
}