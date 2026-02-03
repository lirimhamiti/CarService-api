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
    private readonly IGarageCarRepository _garageCars;

    public CreateCarHandler(ICarRepository cars, IGarageRepository garages, ICarOwnerTokenRepository tokens, IGarageCarRepository garageCars)
    {
        _cars = cars;
        _garages = garages;
        _tokens = tokens;
        _garageCars = garageCars;
    }

    public async Task<CarDto> Handle(CreateCarCommand cmd, CancellationToken ct = default)
    {
        var garage = await _garages.GetByIdAsync(cmd.GarageId, ct);
        if (garage is null)
            throw new ArgumentException("Garage not found.");

        if (garage.Status != GarageStatus.Approved)
            throw new ArgumentException("Garage is not approved.");

        var plate = (cmd.PlateNumber ?? "").Trim().ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(plate))
            throw new ArgumentException("Plate number is required.");

        var vin = (cmd.Vin ?? "").Trim().ToUpperInvariant();
        if (string.IsNullOrWhiteSpace(vin))
            throw new ArgumentException("VIN is required.");

        if (vin.Length != 17)
            throw new ArgumentException("VIN must be exactly 17 characters.");

        var car = await _cars.GetByVinAsync(vin, ct);

        if (car is null)
        {
            car = new Car(plate, vin);
            await _cars.AddAsync(car, ct);
            await _cars.SaveChangesAsync(ct);

            var rawToken = _tokens.GenerateRawToken();
            var tokenHash = _tokens.HashToken(rawToken);

            var carToken = CarOwnerToken.Create(car.Id, tokenHash);
            await _tokens.AddAsync(carToken, ct);
            await _tokens.SaveChangesAsync(ct);
        }
        else
        {

            if (!string.Equals(car.PlateNumber, plate, StringComparison.OrdinalIgnoreCase))
            {
                car.UpdatePlate(plate);
                await _cars.SaveChangesAsync(ct);
            }
        }


        if (!await _garageCars.ExistsAsync(cmd.GarageId, car.Id, ct))
        {
            await _garageCars.AddAsync(new GarageCar(cmd.GarageId, car.Id), ct);
            await _garageCars.SaveChangesAsync(ct);
        }

        return new CarDto(
            car.Id,
            car.PlateNumber,
            car.Vin,
            car.CreatedAt
        );
    }

}
