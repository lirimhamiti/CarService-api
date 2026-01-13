using CarService.Application.Abstractions;
using CarService.Application.Cars.Dtos;

namespace CarService.Application.Cars.Queries;

public sealed class GetCarsByGarageHandler
{
    private readonly ICarRepository _cars;

    public GetCarsByGarageHandler(ICarRepository cars)
    {
        _cars = cars;
    }

    public async Task<IReadOnlyList<CarDto>> Handle(Guid garageId, CancellationToken ct = default)
    {
        var cars = await _cars.GetByGarageIdAsync(garageId, ct);

        return cars
            .Select(c => new CarDto(
                c.Id,
                c.PlateNumber,
                c.Vin,
                c.GarageId,
                c.CreatedAt
            ))
            .ToList();
    }
}
