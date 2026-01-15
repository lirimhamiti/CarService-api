using CarService.Application.Owners.Queries;
using Microsoft.AspNetCore.Mvc;

namespace CarService.Api.Endpoints;

public static class OwnerEndpoints
{
    public static WebApplication MapOwnerEndpoints(this WebApplication app)
    {
        var owner = app.MapGroup("/owner");

        owner.MapGet("/cars/by-token/{token}", async (
            string token,
            [FromServices] GetCarByTokenHandler handler,
            CancellationToken ct) =>
        {
            var dto = await handler.Handle(token, ct);
            return dto is null ? Results.NotFound() : Results.Ok(dto);
        });

        owner.MapGet("/cars/by-vin/{vin}", async (
            string vin,
            [FromServices] GetCarByVinHandler handler,
            CancellationToken ct) =>
        {
            var dto = await handler.Handle(vin, ct);
            return dto is null ? Results.NotFound() : Results.Ok(dto);
        });

        owner.MapGet("/cars/by-token/{token}/services", async (
            string token,
            [FromServices] GetCarServicesByTokenHandler handler,
            CancellationToken ct) =>
        {
            var list = await handler.Handle(token, ct);
            return list is null ? Results.NotFound() : Results.Ok(list);
        });

        owner.MapGet("/cars/by-vin/{vin}/services", async (
            string vin,
            [FromServices] GetCarServicesByVinHandler handler,
            CancellationToken ct) =>
        {
            var list = await handler.Handle(vin, ct);
            return list is null ? Results.NotFound() : Results.Ok(list);
        });

        return app;
    }
}
