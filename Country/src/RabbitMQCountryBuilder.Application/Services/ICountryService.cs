using RabbitMQCountryBuilder.Application.DTO_s;
using RabbitMQCountryBuilder.Domain.Entities;

namespace RabbitMQCountryBuilder.Application.Services;

public interface ICountryService
{
    Task<Country> AddAsync(CountryDto dto);
}