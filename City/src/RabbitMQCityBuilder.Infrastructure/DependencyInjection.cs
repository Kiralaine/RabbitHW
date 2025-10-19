using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQCityBuilder.Application.Interfaces;
using RabbitMQCityBuilder.Infrastructure.Persistence.Repository;

namespace RabbitMQCityBuilder.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<ICityRepository, CityRepository>();

        var rabbitMqSection = configuration.GetSection("RabbitMQ");
        var hostName = rabbitMqSection["HostName"];
        var userName = rabbitMqSection["UserName"];
        var password = rabbitMqSection["Password"];
        var queueName = rabbitMqSection["QueueName"];

        var rabbitMQSettings = new Persistence.Settings.RabbitMqSettings
        {
            HostName = hostName,
            UserName = userName,
            Password = password,
            QueueName = queueName
        };

        services.AddSingleton(rabbitMQSettings);


        return services;
    }
}