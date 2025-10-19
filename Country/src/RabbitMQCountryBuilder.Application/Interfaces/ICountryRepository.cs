using RabbitMQCountryBuilder.Application.DTO_s;
using RabbitMQCountryBuilder.Domain.Entities;

namespace RabbitMQCountryBuilder.Application.Interfaces;

public interface ICountryRepository
{
    Task<Country> AddAsync(Country file);
}
