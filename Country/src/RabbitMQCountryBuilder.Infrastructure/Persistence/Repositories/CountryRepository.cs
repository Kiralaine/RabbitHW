using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQCountryBuilder.Application.Interfaces;
using RabbitMQCountryBuilder.Domain.Entities;
using RabbitMQCountryBuilder.Infrastructure.Persistence.Settings;

namespace RabbitMQCountryBuilder.Infrastructure.Persistence.Repository;
public class CountryRepository : ICountryRepository
{
    private readonly List<Country> _storage = new();
    private readonly RabbitMqSettings _settings;

    public CountryRepository(RabbitMqSettings settings)
    {
        _settings = settings;
    }

    public Task<Country> AddAsync(Country file)
    {
        _storage.Add(file);
        PublishToQueue("Add", file);
        return Task.FromResult(file);
    }

    private async Task PublishToQueue(string action, Country record)
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