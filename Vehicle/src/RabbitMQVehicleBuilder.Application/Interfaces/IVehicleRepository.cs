using RabbitMQVehicleBuilder.Application.DTO_s;
using RabbitMQVehicleBuilder.Domain.Entities;

namespace RabbitMQVehicleBuilder.Application.Interfaces;

public interface IVehicleRepository
{
    Task<Vehicle> AddAsync(Vehicle file);
}
