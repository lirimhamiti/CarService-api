using CarService.Application.Abstractions;
using CarService.Application.Garages.Dtos;

namespace CarService.Application.Garages.Commands;

public sealed class GetPendingGaragesHandler
{
    private readonly IGarageRepository _garages;

    public GetPendingGaragesHandler(IGarageRepository garages)
    {
        _garages = garages;
    }

    public async Task<List<GarageDto>> Handle(CancellationToken ct = default)
    {
        var garages = await _garages.GetPendingAsync(ct);

        return garages.Select(g => new GarageDto(
            g.Id,
            g.Name,
            g.City,
            g.Email,
            g.Username,
            g.Status.ToString()
        )).ToList();
    }
}
