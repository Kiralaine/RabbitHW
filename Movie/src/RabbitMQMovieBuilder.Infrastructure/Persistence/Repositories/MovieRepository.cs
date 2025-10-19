using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQMovieBuilder.Application.Interfaces;
using RabbitMQMovieBuilder.Domain.Entities;
using RabbitMQMovieBuilder.Infrastructure.Persistence.Settings;

namespace RabbitMQMovieBuilder.Infrastructure.Persistence.Repository;
public class MovieRepository : IMovieRepository
{
    private readonly List<Movie> _storage = new();
    private readonly RabbitMqSettings _settings;

    public MovieRepository(RabbitMqSettings settings)
    {
        _settings = settings;
    }

    public Task<Movie> AddAsync(Movie file)
    {
        _storage.Add(file);
        PublishToQueue("Add", file);
        return Task.FromResult(file);
    }

    private async Task PublishToQueue(string action, Movie record)
    {
        var factory = new ConnectionFactory()
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password
        };

        using var connection = await factory.CreateConnectionAsync();
        using var channel = await connection.CreateChannelAsync();

        await channel.QueueDeclareAsync(
            queue: _settings.QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        var message = JsonSerializer.Serialize(record);
        var body = Encoding.UTF8.GetBytes(message);

        var basicProperties = new BasicProperties();
        await channel.BasicPublishAsync(
            exchange: "",
            routingKey: _settings.QueueName,
            basicProperties: basicProperties,
            mandatory: false,
            body: body
        );
    }
}