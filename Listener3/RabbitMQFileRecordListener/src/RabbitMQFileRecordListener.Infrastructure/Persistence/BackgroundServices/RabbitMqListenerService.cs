using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQFileRecordListener.Application.Sevices;
using RabbitMQFileRecordListener.Domain.Entities;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;

namespace RabbitMQFileRecordListener.Infrastructure.Persistence.Services;

public class RabbitMqListenerService : BackgroundService
{
    private readonly IServiceProvider _serviceProvider;

    public RabbitMqListenerService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var fcountryy = new ConnectionFcountryy() { HostName = "localhost" };
        var connection = await fcountryy.CreateConnectionAsync();
        var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: "FileRecord_queue",
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
            var student = JsonSerializer.Deserialize<FileRecord>(message);

            if (student != null)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var fileRecordService = scope.ServiceProvider.GetRequiredService<IFileRecordService>();
                    await fileRecordService.CreateAsync(student);
                }
            }
        };

        await channel.BasicConsumeAsync(
            queue: "FileRecord_queue",
            autoAck: true,
            consumer: consumer
        );
    }
}
