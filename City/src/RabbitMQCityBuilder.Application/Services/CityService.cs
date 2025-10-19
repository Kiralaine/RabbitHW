using RabbitMQCityBuilder.Application.DTO_s;
using RabbitMQCityBuilder.Application.Interfaces;
using RabbitMQCityBuilder.Domain.Entities;

namespace RabbitMQCityBuilder.Application.Services;

public class CityService : ICityService
{
    private readonly ICityRepository _cityRepository;

    public CityService(ICityRepository cityRepository)
    {
        _cityRepository = cityRepository;
    }

    public async Task<City> AddAsync(CityDto dto)
    {
        var city = new City
        {
            Id = Guid.NewGuid(),
            FileName = dto.FileName,
            FilePath = dto.FilePath,
            FileSize = dto.FileSize,
            ContentType = dto.ContentType,
            UploadedBy = dto.UploadedBy,
            Description = dto.Description,
            IsPublic = dto.IsPublic,
            Hash = dto.Hash,
            UploadedAt = DateTime.UtcNow
        };

        return await _cityRepository.AddAsync(city);
    }
}