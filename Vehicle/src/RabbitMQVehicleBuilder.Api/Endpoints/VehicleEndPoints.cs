using RabbitMQVehicleBuilder.Application.DTO_s;
using RabbitMQVehicleBuilder.Application.Services;

namespace RabbitMQVehicleBuilder.Api.Endpoints;

public static class VehicleEndpoints
{
    public static void MapVehicleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/files");

        group.MapPost("/", async (IVehicleService service, VehicleDto dto) =>
        {
            var result = await service.AddAsync(dto);
            return Results.Created($"/api/files/{result.Id}", result);
        });
    }
}
