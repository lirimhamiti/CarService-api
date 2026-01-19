using CarService.Application.Abstractions;
using CarService.Application.Cars.Commands;
using CarService.Application.Cars.Queries;
using QRCoder;

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



        app.MapGet("/cars/{carId:guid}/qr", async (
            Guid carId,
            ICarOwnerTokenRepository tokens,
            IConfiguration cfg,
            CancellationToken ct) =>
        {
            var t = await tokens.GetActiveByCarIdAsync(carId, ct);
            if (t is null)
                return Results.NotFound("No active token for this car.");

            var payload = $"/public?token={t.TokenHash}";


            byte[] png;
            using (var gen = new QRCodeGenerator())
            using (var data = gen.CreateQrCode(payload, QRCodeGenerator.ECCLevel.Q))
            {
                var qr = new PngByteQRCode(data);
                png = qr.GetGraphic(pixelsPerModule: 12);
            }

            return Results.File(png, "image/png", $"car-{carId}-qr.png");
        });


        return app;
    }
}
