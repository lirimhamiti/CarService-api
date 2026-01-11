using CarService.Application.Garages.Commands;

namespace CarService.Api.Endpoints;

public static class AdminEndpoints
{
    public static WebApplication MapAdminEndpoints(this WebApplication app)
    {
        var admin = app.MapGroup("/admin");

        admin.MapGet("/garages/pending", async (GetPendingGaragesHandler handler, CancellationToken ct) =>
        {
            var dtos = await handler.Handle(ct);
            return Results.Ok(dtos);
        });

        admin.MapPost("/garages/{id:guid}/approve", async (Guid id, ApproveGarageHandler handler, CancellationToken ct) =>
        {
            var dto = await handler.Handle(id, ct);
            return dto is null ? Results.NotFound() : Results.Ok(dto);
        });

        admin.MapPost("/garages/{id:guid}/reject", async (Guid id, RejectGarageHandler handler, CancellationToken ct) =>
        {
            var dto = await handler.Handle(id, ct);
            return dto is null ? Results.NotFound() : Results.Ok(dto);
        });

        return app;
    }
}
