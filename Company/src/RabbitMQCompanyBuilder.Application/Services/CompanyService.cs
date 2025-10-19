using RabbitMQCompanyBuilder.Application.DTO_s;
using RabbitMQCompanyBuilder.Application.Interfaces;
using RabbitMQCompanyBuilder.Domain.Entities;

namespace RabbitMQCompanyBuilder.Application.Services;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<Company> AddAsync(CompanyDto dto)
    {
        var company = new Company
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

        return await _companyRepository.AddAsync(company);
    }
}