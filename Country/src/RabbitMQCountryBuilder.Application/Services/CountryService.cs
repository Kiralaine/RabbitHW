using RabbitMQCountryBuilder.Application.DTO_s;
using RabbitMQCountryBuilder.Application.Interfaces;
using RabbitMQCountryBuilder.Domain.Entities;

namespace RabbitMQCountryBuilder.Application.Services;

public class CountryService : ICountryService
{
    private readonly ICountryRepository _countryRepository;

    public CountryService(ICountryRepository countryRepository)
    {
        _countryRepository = countryRepository;
    }

    public async Task<Country> AddAsync(CountryDto dto)
    {
        var country = new Country
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

        return await _countryRepository.AddAsync(country);
    }
}