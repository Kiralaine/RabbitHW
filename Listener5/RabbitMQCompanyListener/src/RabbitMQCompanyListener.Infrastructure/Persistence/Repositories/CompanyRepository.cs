using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RabbitMQCompanyListener.Application.Interfaces;
using RabbitMQCompanyListener.Domain.Entities;

namespace RabbitMQCompanyListener.Infrastructure.Persistence.Repositories;

public class CompanyRepository : ICompanyRepository
{
    private readonly AppDbContext _context;

    public CompanyRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Company company)
    {
        _context.Companys.Add(company);
        await _context.SaveChangesAsync();
    }

}
