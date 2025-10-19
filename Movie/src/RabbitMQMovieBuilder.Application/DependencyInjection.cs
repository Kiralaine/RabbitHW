using Microsoft.Extensions.DependencyInjection;
using RabbitMQMovieBuilder.Application.Services;

namespace RabbitMQMovieBuilder.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IMovieService, MovieService>();

        return services;
    }
}
