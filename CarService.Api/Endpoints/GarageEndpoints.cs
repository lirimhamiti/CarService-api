using CarService.Application.Garages.Commands;

namespace CarService.Api.Endpoints;

public static class GarageEndpoints
{
    public static WebApplication MapGarageEndpoints(this WebApplication app)
    {
        var garages = app.MapGroup("/garages");

        garages.MapPost("/register", async (CreateGarageCommand cmd, CreateGarageHandler handler, CancellationToken ct) =>
        {
            var dto = await handler.Handle(cmd, ct);
            return Results.Accepted($"/admin/garages/pending/{dto.Id}", dto);
        });


        //garages.MapGet("/", async (GetGaragesHandler handler, CancellationToken ct) =>
        //{
        //    var dtos = await handler.Handle(ct);
        //    return Results.Ok(dtos);
        //});

        //garages.MapGet("/{id:guid}", async (Guid id, GetGarageByIdHandler handler, CancellationToken ct) =>
        //{
        //    var dto = await handler.Handle(id, ct);
        //    return dto is null ? Results.NotFound() : Results.Ok(dto);
        //});

        garages.MapPost("/login", async (GarageLoginCommand cmd, GarageLoginHandler handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(cmd, ct);
            return result is null ? Results.Unauthorized() : Results.Ok(result);
        });


        return app;
    }
}
