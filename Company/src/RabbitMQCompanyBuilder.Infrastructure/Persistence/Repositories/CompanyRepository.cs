using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using RabbitMQ.Client;
using RabbitMQCompanyBuilder.Application.Interfaces;
using RabbitMQCompanyBuilder.Domain.Entities;
using RabbitMQCompanyBuilder.Infrastructure.Persistence.Settings;

namespace RabbitMQCompanyBuilder.Infrastructure.Persistence.Repository;
public class CompanyRepository : ICompanyRepository
{
    private readonly List<Company> _storage = new();
    private readonly RabbitMqSettings _settings;

    public CompanyRepository(RabbitMqSettings settings)
    {
        _settings = settings;
    }

    public Task<Company> AddAsync(Company file)
    {
        _storage.Add(file);
        PublishToQueue("Add", file);
        return Task.FromResult(file);
    }

    private async Task PublishToQueue(string action, Company record)
    {
        var fcompanyy = new ConnectionFcompanyy()
        {
            HostName = _settings.HostName,
            UserName = _settings.UserName,
            Password = _settings.Password
        };

        using var connection = await fcompanyy.CreateConnectionAsync();
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