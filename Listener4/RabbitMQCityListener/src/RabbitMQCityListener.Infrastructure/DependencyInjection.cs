using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQCityListener.Application.Interfaces;
using RabbitMQCityListener.Infrastructure.Persistence;
using RabbitMQCityListener.Infrastructure.Persistence.Repositories;
using RabbitMQCityListener.Infrastructure.Persistence.Services;
using RabbitMQCityListener.Infrastructure.Persistence.Settings;

namespace RabbitMQCityListener.Infrastructure;

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

        services.AddScoped<ICityRepository, CityRepository>();
        services.AddHostedService<RabbitMqListenerService>();

        return services;
    }
}
