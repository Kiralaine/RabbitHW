using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQCompanyListener.Application.Sevices;
using RabbitMQCompanyListener.Domain.Entities;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;

namespace RabbitMQCompanyListener.Infrastructure.Persistence.Services;

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
            queue: "Company_queue",
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
            var student = JsonSerializer.Deserialize<Company>(message);

            if (student != null)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var companyService = scope.ServiceProvider.GetRequiredService<ICompanyService>();
                    await companyService.CreateAsync(student);
                }
            }
        };

        await channel.BasicConsumeAsync(
            queue: "Company_queue",
            autoAck: true,
            consumer: consumer
        );
    }
}
