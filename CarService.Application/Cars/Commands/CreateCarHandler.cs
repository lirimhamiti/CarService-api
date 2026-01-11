using CarService.Application.Abstractions;
using CarService.Application.Cars.Dtos;
using CarService.Domain.Entities;

namespace CarService.Application.Cars.Commands;

public sealed class CreateCarHandler
{
    private readonly ICarRepository _cars;
    private readonly IGarageRepository _garages;

    public CreateCarHandler(ICarRepository cars, IGarageRepository garages)
    {
        _cars = cars;
        _garages = garages;
    }

    public async Task<CarDto> Handle(CreateCarCommand cmd, CancellationToken ct = default)
    {
        var garage = await _garages.GetByIdAsync(cmd.GarageId, ct);
        if (garage is null)
            throw new ArgumentException("Garage not found.");

        var vin = (cmd.Vin ?? string.Empty).Trim();
        var plate = cmd.PlateNumber; 

        if (!string.IsNullOrWhiteSpace(vin))
        {
            vin = vin.ToUpperInvariant();
            if (await _cars.VinExistsAsync(vin, ct))
                throw new ArgumentException("A car with this VIN already exists.");
        }

        var car = new Car(cmd.GarageId, plate, vin);

        await _cars.AddAsync(car, ct);

        return new CarDto(
            car.Id,
            car.PlateNumber,
            car.Vin,
            car.GarageId,
            car.CreatedAt
        );
    }
}
