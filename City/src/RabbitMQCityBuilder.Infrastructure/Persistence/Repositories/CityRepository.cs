using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQCityBuilder.Application.Interfaces;
using RabbitMQCityBuilder.Domain.Entities;
using RabbitMQCityBuilder.Infrastructure.Persistence.Settings;

namespace RabbitMQCityBuilder.Infrastructure.Persistence.Repository;
public class CityRepository : ICityRepository
{
    private readonly List<City> _storage = new();
    private readonly RabbitMqSettings _settings;

    public CityRepository(RabbitMqSettings settings)
    {
        _settings = settings;
    }

    public Task<City> AddAsync(City file)
    {
        _storage.Add(file);
        PublishToQueue("Add", file);
        return Task.FromResult(file);
    }

    private async Task PublishToQueue(string action, City record)
    {
        var fcityy = new ConnectionFcityy()
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password
        };

        using var connection = await fcityy.CreateConnectionAsync();
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