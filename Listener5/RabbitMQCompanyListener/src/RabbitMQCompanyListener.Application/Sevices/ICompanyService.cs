using RabbitMQCompanyListener.Domain.Entities;

namespace RabbitMQCompanyListener.Application.Sevices;

public interface ICompanyService
{
    Task CreateAsync(Company company);
}
