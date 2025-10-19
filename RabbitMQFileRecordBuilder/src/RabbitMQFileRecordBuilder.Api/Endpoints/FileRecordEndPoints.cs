using RabbitMQFileRecordBuilder.Application.DTO_s;
using RabbitMQFileRecordBuilder.Application.Services;

namespace RabbitMQFileRecordBuilder.Api.Endpoints;

public static class FileRecordEndpoints
{
    public static void MapFileRecordEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/files");

        group.MapPost("/", async (IFileRecordService service, FileRecordDto dto) =>
        {
            var result = await service.AddAsync(dto);
            return Results.Created($"/api/files/{result.Id}", result);
        });
    }
}
