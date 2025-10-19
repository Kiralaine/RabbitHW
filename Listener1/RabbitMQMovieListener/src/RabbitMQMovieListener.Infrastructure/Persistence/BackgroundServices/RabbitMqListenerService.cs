using Microsoft.Extensions.DependencyInjection;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQMovieListener.Application.Sevices;
using RabbitMQMovieListener.Domain.Entities;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Hosting;

namespace RabbitMQMovieListener.Infrastructure.Persistence.Services;

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
            queue: "Movie_queue",
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
            var student = JsonSerializer.Deserialize<Movie>(message);

            if (student != null)
            {
                using (var scope = _serviceProvider.CreateScope())
                {
                    var movieService = scope.ServiceProvider.GetRequiredService<IMovieService>();
                    await movieService.CreateAsync(student);
                }
            }
        };

        await channel.BasicConsumeAsync(
            queue: "Movie_queue",
            autoAck: true,
            consumer: consumer
        );
    }
}
