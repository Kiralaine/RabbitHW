using RabbitMQVehicleListener.Application.Interfaces;
using RabbitMQVehicleListener.Domain.Entities;

namespace RabbitMQVehicleListener.Application.Sevices;

public class VehicleService : IVehicleService
{
    private readonly IVehicleRepository _repository;

    public VehicleService(IVehicleRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(Vehicle vehicle)
    {
        await _repository.AddAsync(vehicle);
    }
}
