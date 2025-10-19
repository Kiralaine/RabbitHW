using RabbitMQCityBuilder.Application.DTO_s;
using RabbitMQCityBuilder.Domain.Entities;

namespace RabbitMQCityBuilder.Application.Interfaces;

public interface ICityRepository
{
    Task<City> AddAsync(City file);
}
