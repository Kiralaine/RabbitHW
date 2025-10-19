using RabbitMQVehicleListener.Domain.Entities;

namespace RabbitMQVehicleListener.Application.Interfaces;

public interface IVehicleRepository
{
    Task AddAsync(Vehicle vehicle);
}