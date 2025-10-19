using RabbitMQCityListener.Domain.Entities;

namespace RabbitMQCityListener.Application.Interfaces;

public interface ICityRepository
{
    Task AddAsync(City city);
}