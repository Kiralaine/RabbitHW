using Microsoft.Extensions.DependencyInjection;
using RabbitMQCityBuilder.Application.Services;

namespace RabbitMQCityBuilder.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICityService, CityService>();

        return services;
    }
}
