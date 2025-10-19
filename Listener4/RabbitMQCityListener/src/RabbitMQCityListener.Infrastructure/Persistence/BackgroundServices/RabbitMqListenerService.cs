using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQCityListener.Application.Sevices;
using RabbitMQCityListener.Domain.Entities;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;

namespace RabbitMQCityListener.Infrastructure.Persistence.Services;

public class RabbitMqListenerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public RabbitMqListenerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        var connection = await factory.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "City_queue",
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var consumer = new AsyncEventingBasicConsumer(channel);

        consumer.ReceivedAsync += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var student = JsonSerializer.Deserialize<City>(message);

            if (student != null)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var cityService = scope.ServiceProvider.GetRequiredService<ICityService>();
                    await cityService.CreateAsync(student);
                }
            }
        };

        await channel.BasicConsumeAsync(
            queue: "City_queue",
            autoAck: true,
            consumer: consumer
        );
    }
}
