using Microsoft.Extensions.DependencyInjection;
using RabbitMQVehicleBuilder.Application.Services;

namespace RabbitMQVehicleBuilder.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IVehicleService, VehicleService>();

        return services;
    }
}
