using RabbitMQCompanyBuilder.Application.DTO_s;
using RabbitMQCompanyBuilder.Application.Services;

namespace RabbitMQCompanyBuilder.Api.Endpoints;

public static class CompanyEndpoints
{
    public static void MapCompanyEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/api/files");

        group.MapPost("/", async (ICompanyService service, CompanyDto dto) =>
        {
            var result = await service.AddAsync(dto);
            return Results.Created($"/api/files/{result.Id}", result);
        });
    }
}
