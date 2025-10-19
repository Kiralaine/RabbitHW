using RabbitMQActorBuilder.Application.DTO_s;
using RabbitMQActorBuilder.Application.Services;

namespace RabbitMQActorBuilder.Api.Endpoints;

public static class ActorEndpoints
{
    public static void MapActorEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/files");

        group.MapPost("/", async (IActorService service, ActorDto dto) =>
        {
            var result = await service.AddAsync(dto);
            return Results.Created($"/api/files/{result.Id}", result);
        });
    }
}
