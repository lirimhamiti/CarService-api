using CarService.Application.Abstractions;
using CarService.Application.Garages.Dtos;

namespace CarService.Application.Garages.Commands;

public sealed class GetGaragesHandler
{
    private readonly IGarageRepository _garages;

    public GetGaragesHandler(IGarageRepository garages)
    {
        _garages = garages;
    }

    public async Task<IReadOnlyList<GarageDto>> Handle(CancellationToken ct = default)
    {
        var list = await _garages.GetAllAsync(ct);
        return list.Select(x => new GarageDto(x.Id, x.Name, x.City)).ToList();
    }
}
