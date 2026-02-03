using CarService.Application.Abstractions;
using CarService.Application.Owners.Dtos;

namespace CarService.Application.Owners.Queries;

public sealed class GetCarByVinHandler
{
    private readonly ICarRepository _cars;

    public GetCarByVinHandler(ICarRepository cars)
    {
        _cars = cars;
    }

    public async Task<OwnerCarDto?> Handle(string vin, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(vin))
            return null;

        var car = await _cars.GetByVinAsync(vin.Trim(), ct);
        if (car is null)
            return null;

        return new OwnerCarDto(
            car.Id,
            car.PlateNumber,
            car.Vin
        );
    }
}
