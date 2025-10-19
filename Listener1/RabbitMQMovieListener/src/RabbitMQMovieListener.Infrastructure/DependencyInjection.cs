using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQMovieListener.Application.Interfaces;
using RabbitMQMovieListener.Infrastructure.Persistence;
using RabbitMQMovieListener.Infrastructure.Persistence.Repositories;
using RabbitMQMovieListener.Infrastructure.Persistence.Services;
using RabbitMQMovieListener.Infrastructure.Persistence.Settings;

namespace RabbitMQMovieListener.Infrastructure;

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

        services.AddScoped<IMovieRepository, MovieRepository>();
        services.AddHostedService<RabbitMqListenerService>();

        return services;
    }
}
