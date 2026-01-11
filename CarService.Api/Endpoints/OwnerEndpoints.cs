using CarService.Application.Owners.Queries;

namespace CarService.Api.Endpoints;

public static class OwnerEndpoints
{
    public static WebApplication MapOwnerEndpoints(this WebApplication app)
    {
        var owner = app.MapGroup("/owner");

        owner.MapGet("/cars/by-token/{token}", async (string token, GetCarByTokenHandler handler, CancellationToken ct) =>
        {
            var dto = await handler.Handle(token, ct);
            return dto is null ? Results.NotFound() : Results.Ok(dto);
        });

        return app;
    }
}
