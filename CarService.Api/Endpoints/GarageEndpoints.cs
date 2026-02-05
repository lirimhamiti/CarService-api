using CarService.Application.Garages.Commands;
using CarService.Application.Garages.Dtos;
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



        garages.MapPost("/login", async (
       GarageLoginCommand cmd,
       GarageLoginHandler handler,
       CancellationToken ct) =>
        {
            var (result, failure) = await handler.Handle(cmd, ct);

            if (result is not null)
                return Results.Ok(result);

            return failure!.Code switch
            {
                GarageLoginFailure.PendingApproval => Results.Problem(
                    title: "Pending approval",
                    detail: failure.Message,
                    statusCode: StatusCodes.Status403Forbidden),

                GarageLoginFailure.Rejected => Results.Problem(
                    title: "Rejected",
                    detail: failure.Message,
                    statusCode: StatusCodes.Status403Forbidden),

                _ => Results.Problem(
                    title: "Unauthorized",
                    detail: failure.Message,
                    statusCode: StatusCodes.Status401Unauthorized),
            };
        });



        return app;
    }
}
