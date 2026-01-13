using CarService.Application.Cars.Commands;
using CarService.Application.Cars.Queries;

namespace CarService.Api.Endpoints;

public static class CarEndpoints
{
    public static WebApplication MapCarEndpoints(this WebApplication app)
    {
        app.MapPost("/garages/{garageId:guid}/cars", async (
            Guid garageId,
            CreateCarCommand cmd,
            CreateCarHandler handler,
            CancellationToken ct) =>
        {
            var dto = await handler.Handle(cmd with { GarageId = garageId }, ct);
            return Results.Created($"/cars/{dto.Id}", dto);
        });

        app.MapGet("/garages/{garageId:guid}/cars", async (
    Guid garageId,
    GetCarsByGarageHandler handler,
    CancellationToken ct) =>
        {
            var dtos = await handler.Handle(garageId, ct);
            return Results.Ok(dtos);
        });


        return app;
    }
}
