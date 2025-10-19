using RabbitMQCompanyBuilder.Application.DTO_s;
using RabbitMQCompanyBuilder.Domain.Entities;

namespace RabbitMQCompanyBuilder.Application.Services;

public interface ICompanyService
{
    Task<Company> AddAsync(CompanyDto dto);
}