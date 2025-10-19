using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQVehicleListener.Application.Interfaces;
using RabbitMQVehicleListener.Domain.Entities;

namespace RabbitMQVehicleListener.Infrastructure.Persistence.Repositories;

public class VehicleRepository : IVehicleRepository
{
    private readonly AppDbContext _context;

    public VehicleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Vehicle vehicle)
    {
        _context.Vehicles.Add(vehicle);
        await _context.SaveChangesAsync();
    }

}
