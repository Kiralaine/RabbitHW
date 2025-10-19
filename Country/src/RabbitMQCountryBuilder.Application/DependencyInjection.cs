using Microsoft.Extensions.DependencyInjection;
using RabbitMQCountryBuilder.Application.Services;

namespace RabbitMQCountryBuilder.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICountryService, CountryService>();

        return services;
    }
}
