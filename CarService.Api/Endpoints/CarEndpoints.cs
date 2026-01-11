using CarService.Application.Cars.Commands;

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

        return app;
    }
}
