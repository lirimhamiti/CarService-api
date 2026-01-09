using CarService.Application.Abstractions;
using CarService.Application.Garages.Dtos;

namespace CarService.Application.Garages.Commands;

public sealed class GetGarageByIdHandler
{
    private readonly IGarageRepository _garages;

    public GetGarageByIdHandler(IGarageRepository garages)
    {
        _garages = garages;
    }

    public async Task<GarageDto?> Handle(Guid id, CancellationToken ct = default)
    {
        var garage = await _garages.GetByIdAsync(id, ct);
        return garage is null ? null : new GarageDto(garage.Id, garage.Name, garage.City);
    }
}
