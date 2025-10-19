using Microsoft.Extensions.DependencyInjection;
using RabbitMQActorBuilder.Application.Services;

namespace RabbitMQActorBuilder.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IActorService, ActorService>();

        return services;
    }
}
