using CarService.Domain.Entities;
using CarService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarService.Api.Endpoints;

public static class ServiceRecordEndpoints
{
    public static IEndpointRouteBuilder MapServiceRecordEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/garages/{garageId:guid}/cars/{carId:guid}/services")
            .WithTags("Service Records");

        group.MapGet("/", async (
            Guid garageId,
            Guid carId,
            CarServiceDbContext db,
            CancellationToken ct) =>
        {
            var carOk = await db.Cars.AnyAsync(c => c.Id == carId && c.GarageId == garageId, ct);
            if (!carOk) return Results.NotFound("Car not found for this garage.");

            var items = await db.ServiceRecords
                .Where(x => x.CarId == carId && x.GarageId == garageId)
                .OrderByDescending(x => x.ServiceDate)
                .Select(x => new
                {
                    id = x.Id,
                    carId = x.CarId,
                    garageId = x.GarageId,
                    serviceDate = x.ServiceDate,
                    mileage = x.Mileage,
                    notes = x.Notes,
                    createdAt = x.CreatedAt
                })
                .ToListAsync(ct);

            return Results.Ok(items);
        });

        group.MapPost("/", async (
            Guid garageId,
            Guid carId,
            CreateServiceRecordRequest req,
            CarServiceDbContext db,
            CancellationToken ct) =>
        {
            var carOk = await db.Cars.AnyAsync(c => c.Id == carId && c.GarageId == garageId, ct);
            if (!carOk) return Results.NotFound("Car not found for this garage.");

            if (req.Mileage < 0) return Results.BadRequest("Mileage must be >= 0.");

            var entity = new ServiceRecord(
                carId: carId,
                garageId: garageId,
                serviceDate: req.ServiceDate,
                mileage: req.Mileage,
                notes: string.IsNullOrWhiteSpace(req.Notes) ? null : req.Notes.Trim()
            );

            db.ServiceRecords.Add(entity);
            await db.SaveChangesAsync(ct);

            return Results.Ok(new
            {
                id = entity.Id,
                carId = entity.CarId,
                garageId = entity.GarageId,
                serviceDate = entity.ServiceDate,
                mileage = entity.Mileage,
                notes = entity.Notes,
                createdAt = entity.CreatedAt
            });
        });

        return app;
    }
}

public sealed record CreateServiceRecordRequest(
    DateTime ServiceDate,
    int Mileage,
    string? Notes
);
