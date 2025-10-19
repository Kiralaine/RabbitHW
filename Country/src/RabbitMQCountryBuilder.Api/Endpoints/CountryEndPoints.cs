using RabbitMQCountryBuilder.Application.DTO_s;
using RabbitMQCountryBuilder.Application.Services;

namespace RabbitMQCountryBuilder.Api.Endpoints;

public static class CountryEndpoints
{
    public static void MapCountryEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/files");

        group.MapPost("/", async (ICountryService service, CountryDto dto) =>
        {
            var result = await service.AddAsync(dto);
            return Results.Created($"/api/files/{result.Id}", result);
        });
    }
}
