using RabbitMQVehicleListener.Domain.Entities;

namespace RabbitMQVehicleListener.Application.Sevices;

public interface IVehicleService
{
    Task CreateAsync(Vehicle vehicle);
}
