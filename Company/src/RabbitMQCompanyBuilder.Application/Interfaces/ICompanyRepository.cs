using RabbitMQCompanyBuilder.Application.DTO_s;
using RabbitMQCompanyBuilder.Domain.Entities;

namespace RabbitMQCompanyBuilder.Application.Interfaces;

public interface ICompanyRepository
{
    Task<Company> AddAsync(Company file);
}
