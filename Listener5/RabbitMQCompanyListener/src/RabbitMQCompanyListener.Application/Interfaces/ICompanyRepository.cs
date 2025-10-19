using RabbitMQCompanyListener.Domain.Entities;

namespace RabbitMQCompanyListener.Application.Interfaces;

public interface ICompanyRepository
{
    Task AddAsync(Company company);
}