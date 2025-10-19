using RabbitMQMovieBuilder.Application.DTO_s;
using RabbitMQMovieBuilder.Application.Services;

namespace RabbitMQMovieBuilder.Api.Endpoints;

public static class MovieEndpoints
{
    public static void MapMovieEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/files");

        group.MapPost("/", async (IMovieService service, MovieDto dto) =>
        {
            var result = await service.AddAsync(dto);
            return Results.Created($"/api/files/{result.Id}", result);
        });
    }
}
