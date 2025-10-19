using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQVehicleBuilder.Application.Interfaces;
using RabbitMQVehicleBuilder.Domain.Entities;
using RabbitMQVehicleBuilder.Infrastructure.Persistence.Settings;

namespace RabbitMQVehicleBuilder.Infrastructure.Persistence.Repository;
public class VehicleRepository : IVehicleRepository
{
    private readonly List<Vehicle> _storage = new();
    private readonly RabbitMqSettings _settings;

    public VehicleRepository(RabbitMqSettings settings)
    {
        _settings = settings;
    }

    public Task<Vehicle> AddAsync(Vehicle file)
    {
        _storage.Add(file);
        PublishToQueue("Add", file);
        return Task.FromResult(file);
    }

    private async Task PublishToQueue(string action, Vehicle record)
    {
        var fvehicley = new ConnectionFvehicley()
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password
        };

        using var connection = await fvehicley.CreateConnectionAsync();
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