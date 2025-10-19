using Microsoft.Extensions.DependencyInjection;
using RabbitMQCompanyBuilder.Application.Services;

namespace RabbitMQCompanyBuilder.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ICompanyService, CompanyService>();

        return services;
    }
}
