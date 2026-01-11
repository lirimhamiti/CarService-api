using CarService.Application.Abstractions;
using CarService.Application.Garages.Dtos;

namespace CarService.Application.Garages.Commands;

public sealed class ApproveGarageHandler
{
    private readonly IGarageRepository _garages;

    public ApproveGarageHandler(IGarageRepository garages)
    {
        _garages = garages;
    }

    public async Task<GarageDto?> Handle(Guid id, CancellationToken ct = default)
    {
        var garage = await _garages.GetByIdAsync(id, ct);
        if (garage is null)
            return null;

        garage.Approve();
        await _garages.SaveChangesAsync(ct);

        return new GarageDto(
            garage.Id,
            garage.Name,
            garage.City,
            garage.Email,
            garage.Username,
            garage.Status.ToString()
        );
    }
}
