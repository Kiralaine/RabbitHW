using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using RabbitMQFileRecordListener.Application.Sevices;

namespace RabbitMQFileRecordListener.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IFileRecordService, FileRecordService>();

        return services;
    }
}
