using Microsoft.EntityFrameworkCore;
using RabbitMQFileRecordListener.Infrastructure.Persistence;

namespace RabbitMQFileRecordListener.Api.Configuration;

public static class DatabaseConfiguration
{
    public static void ConfigureDB(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

        builder.Services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(connectionString));
    }
}