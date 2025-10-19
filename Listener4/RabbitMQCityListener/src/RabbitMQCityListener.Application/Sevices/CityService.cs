using RabbitMQCityListener.Application.Interfaces;
using RabbitMQCityListener.Domain.Entities;

namespace RabbitMQCityListener.Application.Sevices;

public class CityService : ICityService
{
    private readonly ICityRepository _repository;

    public CityService(ICityRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(City city)
    {
        await _repository.AddAsync(city);
    }
}
