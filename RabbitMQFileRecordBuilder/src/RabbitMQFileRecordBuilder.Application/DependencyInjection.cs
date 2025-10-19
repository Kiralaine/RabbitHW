using Microsoft.Extensions.DependencyInjection;
using RabbitMQFileRecordBuilder.Application.Services;

namespace RabbitMQFileRecordBuilder.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IFileRecordService, FileRecordService>();

        return services;
    }
}
