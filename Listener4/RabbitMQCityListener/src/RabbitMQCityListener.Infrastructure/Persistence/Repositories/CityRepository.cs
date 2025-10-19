using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQCityListener.Application.Interfaces;
using RabbitMQCityListener.Domain.Entities;

namespace RabbitMQCityListener.Infrastructure.Persistence.Repositories;

public class CityRepository : ICityRepository
{
    private readonly AppDbContext _context;

    public CityRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(City city)
    {
        _context.Citys.Add(city);
        await _context.SaveChangesAsync();
    }

}
