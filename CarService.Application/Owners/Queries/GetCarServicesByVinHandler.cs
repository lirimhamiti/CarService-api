using CarService.Application.Abstractions;
using CarService.Application.Owners.Dtos;

namespace CarService.Application.Owners.Queries;

public sealed class GetCarServicesByVinHandler
{
    private readonly ICarRepository _cars;
    private readonly IServiceRecordRepository _services;

    public GetCarServicesByVinHandler(ICarRepository cars, IServiceRecordRepository services)
    {
        _cars = cars;
        _services = services;
    }

    public async Task<IReadOnlyList<OwnerServiceRecordDto>?> Handle(string vin, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(vin))
            return null;

        var car = await _cars.GetByVinAsync(vin.Trim(), ct);
        if (car is null)
            return null;

        var records = await _services.GetByCarIdAsync(car.Id, ct);

        return records
            .OrderByDescending(x => x.ServiceDate)
            .Select(x => new OwnerServiceRecordDto(
                x.Id,
                x.ServiceDate,
                x.Mileage,
                x.Notes,
                x.CreatedAt
            ))
            .ToList();
    }
}
