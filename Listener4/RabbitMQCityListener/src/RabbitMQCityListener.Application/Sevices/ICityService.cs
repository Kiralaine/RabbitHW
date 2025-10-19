using RabbitMQCityListener.Domain.Entities;

namespace RabbitMQCityListener.Application.Sevices;

public interface ICityService
{
    Task CreateAsync(City city);
}
