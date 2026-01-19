using CarService.Application.Abstractions;
using CarService.Application.Cars.Dtos;
using CarService.Domain.Entities;
using CarService.Domain.Enums;

namespace CarService.Application.Cars.Commands;

public sealed class CreateCarHandler
{
    private readonly ICarRepository _cars;
    private readonly IGarageRepository _garages;
    private readonly ICarOwnerTokenRepository _tokens;

    public CreateCarHandler(ICarRepository cars, IGarageRepository garages, ICarOwnerTokenRepository tokens)
    {
        _cars = cars;
        _garages = garages;
        _tokens = tokens;
    }

    public async Task<CarDto> Handle(CreateCarCommand cmd, CancellationToken ct = default)
    {
        var garage = await _garages.GetByIdAsync(cmd.GarageId, ct);
        if (garage is null)
            throw new ArgumentException("Garage not found.");

        if (garage.Status != GarageStatus.Approved)
            throw new ArgumentException("Garage is not approved.");

        var plate = cmd.PlateNumber.Trim().ToUpperInvariant();

        if (await _cars.PlateExistsAsync(plate, ct))
            throw new ArgumentException("A car with this plate number already exists.");

        var vin = (cmd.Vin ?? "").Trim().ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(vin))
            vin = "N/A";

        if (vin != "N/A" && await _cars.VinExistsAsync(vin, ct))
            throw new ArgumentException("A car with this VIN already exists.");

        var car = new Car(cmd.GarageId, plate, vin);

        await _cars.AddAsync(car, ct);

        await _cars.SaveChangesAsync(ct);

        var rawToken = _tokens.GenerateRawToken();   
        var tokenHash = _tokens.HashToken(rawToken);

        var carToken = CarOwnerToken.Create(car.Id, tokenHash);


        await _tokens.AddAsync(carToken, ct);
        await _tokens.SaveChangesAsync(ct);

        return new CarDto(
            car.Id,
            car.PlateNumber,
            car.Vin,
            car.GarageId,
            car.CreatedAt
        );
    }
}
