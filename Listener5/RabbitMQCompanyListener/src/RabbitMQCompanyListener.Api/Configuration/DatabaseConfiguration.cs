using Microsoft.EntityFrameworkCore;
using RabbitMQCompanyListener.Infrastructure.Persistence;

namespace RabbitMQCompanyListener.Api.Configuration;

public static class DatabaseConfiguration
{
    public static void ConfigureDB(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection");

        builder.Services.AddDbContext<AppDbContext>(options =>
          options.UseSqlServer(connectionString));
    }
}