using CarService.Application.Garages.Commands;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CarService.Api.Endpoints;

public static class GarageEndpoints
{
    public static WebApplication MapGarageEndpoints(this WebApplication app)
    {
        var garages = app.MapGroup("/garages");

        garages.MapPost("/register", async (
      CreateGarageCommand cmd,
      CreateGarageHandler handler,
      CancellationToken ct) =>
        {
            try
            {
                var dto = await handler.Handle(cmd, ct);
                return Results.Accepted($"/admin/garages/pending/{dto.Id}", dto);
            }
            catch (InvalidOperationException ex) when (ex.Message == "USERNAME_TAKEN")
            {
                return Results.Conflict(new
                {
                    code = "USERNAME_TAKEN",
                    message = "This username is already taken."
                });
            }
            catch (DbUpdateException ex) when (ex.InnerException is PostgresException pg && pg.SqlState == "23505")
            {
                return Results.Conflict(new
                {
                    code = "USERNAME_TAKEN",
                    message = "This username is already taken."
                });
            }
        });



        garages.MapPost("/login", async (GarageLoginCommand cmd, GarageLoginHandler handler, CancellationToken ct) =>
        {
            var result = await handler.Handle(cmd, ct);
            return result is null ? Results.Unauthorized() : Results.Ok(result);
        });


        return app;
    }
}
