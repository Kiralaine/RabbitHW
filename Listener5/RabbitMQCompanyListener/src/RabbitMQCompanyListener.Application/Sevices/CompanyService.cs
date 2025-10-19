using RabbitMQCompanyListener.Application.Interfaces;
using RabbitMQCompanyListener.Domain.Entities;

namespace RabbitMQCompanyListener.Application.Sevices;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _repository;

    public CompanyService(ICompanyRepository repository)
    {
        _repository = repository;
    }

    public async Task CreateAsync(Company company)
    {
        await _repository.AddAsync(company);
    }
}
