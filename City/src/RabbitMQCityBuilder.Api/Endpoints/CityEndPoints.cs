using RabbitMQCityBuilder.Application.DTO_s;
using RabbitMQCityBuilder.Application.Services;

namespace RabbitMQCityBuilder.Api.Endpoints;

public static class CityEndpoints
{
    public static void MapCityEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/files");

        group.MapPost("/", async (ICityService service, CityDto dto) =>
        {
            var result = await service.AddAsync(dto);
            return Results.Created($"/api/files/{result.Id}", result);
        });
    }
}
