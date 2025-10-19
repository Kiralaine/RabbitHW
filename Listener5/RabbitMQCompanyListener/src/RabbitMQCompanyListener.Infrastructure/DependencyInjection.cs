using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQCompanyListener.Application.Interfaces;
using RabbitMQCompanyListener.Infrastructure.Persistence;
using RabbitMQCompanyListener.Infrastructure.Persistence.Repositories;
using RabbitMQCompanyListener.Infrastructure.Persistence.Services;
using RabbitMQCompanyListener.Infrastructure.Persistence.Settings;

namespace RabbitMQCompanyListener.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string environmentName)
    {

        var rabbitSettings = new RabbitMqSettings();
        configuration.GetSection("RabbitMq").Bind(rabbitSettings);
        services.AddSingleton(rabbitSettings);

        if (environmentName != "Testing")
        {
            var connectionString = configuration.GetConnectionString("DatabaseConnection");

            services.AddDbContext<AppDbContext>(options =>
              options.UseSqlServer(connectionString));
        }

        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddHostedService<RabbitMqListenerService>();

        return services;
    }
}
