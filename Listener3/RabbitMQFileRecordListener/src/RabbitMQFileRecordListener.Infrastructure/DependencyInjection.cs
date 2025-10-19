using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQFileRecordListener.Application.Interfaces;
using RabbitMQFileRecordListener.Infrastructure.Persistence;
using RabbitMQFileRecordListener.Infrastructure.Persistence.Repositories;
using RabbitMQFileRecordListener.Infrastructure.Persistence.Services;
using RabbitMQFileRecordListener.Infrastructure.Persistence.Settings;

namespace RabbitMQFileRecordListener.Infrastructure;

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

        services.AddScoped<IFileRecordRepository, FileRecordRepository>();
        services.AddHostedService<RabbitMqListenerService>();

        return services;
    }
}
