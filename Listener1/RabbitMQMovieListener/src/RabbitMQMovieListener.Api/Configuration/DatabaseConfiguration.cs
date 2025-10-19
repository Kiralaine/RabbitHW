using Microsoft.EntityFrameworkCore;
using RabbitMQMovieListener.Infrastructure.Persistence;

namespace RabbitMQMovieListener.Api.Configuration;

public static class DatabaseConfiguration
{
    public static void ConfigureDB(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

        builder.Services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(connectionString));
    }
}