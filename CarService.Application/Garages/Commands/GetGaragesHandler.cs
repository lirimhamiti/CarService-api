using CarService.Application.Abstractions;
using CarService.Application.Garages.Dtos;
using CarService.Domain.Enums;

namespace CarService.Application.Garages.Commands;

public sealed class GetGaragesHandler
{
    private readonly IGarageRepository _garages;

    public GetGaragesHandler(IGarageRepository garages)
    {
        _garages = garages;
    }

    public async Task<IReadOnlyList<GarageDto>> Handle(string? status, CancellationToken ct = default)
    {
        var s = (status ?? "all").Trim().ToLowerInvariant();

        var list = s switch
        {
            "pending" => await _garages.GetByStatusAsync(GarageStatus.Pending, ct),
            "approved" => await _garages.GetByStatusAsync(GarageStatus.Approved, ct),
            "rejected" => await _garages.GetByStatusAsync(GarageStatus.Rejected, ct),
            "all" => await _garages.GetAllAsync(ct),
            _ => await _garages.GetAllAsync(ct) 
        };

        return list
            .Select(x => new GarageDto(x.Id, x.Name, x.City, x.Email, x.Username, x.Status.ToString()))
            .ToList();
    }
}
