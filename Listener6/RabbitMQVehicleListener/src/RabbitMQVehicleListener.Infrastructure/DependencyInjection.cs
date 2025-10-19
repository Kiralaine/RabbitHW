using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQVehicleListener.Application.Interfaces;
using RabbitMQVehicleListener.Infrastructure.Persistence;
using RabbitMQVehicleListener.Infrastructure.Persistence.Repositories;
using RabbitMQVehicleListener.Infrastructure.Persistence.Services;
using RabbitMQVehicleListener.Infrastructure.Persistence.Settings;

namespace RabbitMQVehicleListener.Infrastructure;

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

        services.AddScoped<IVehicleRepository, VehicleRepository>();
        services.AddHostedService<RabbitMqListenerService>();

        return services;
    }
}
