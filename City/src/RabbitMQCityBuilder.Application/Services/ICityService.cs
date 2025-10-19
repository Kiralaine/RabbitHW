using RabbitMQCityBuilder.Application.DTO_s;
using RabbitMQCityBuilder.Domain.Entities;

namespace RabbitMQCityBuilder.Application.Services;

public interface ICityService
{
    Task<City> AddAsync(CityDto dto);
}