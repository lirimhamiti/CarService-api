using CarService.Application.Abstractions;
using CarService.Application.Garages.Dtos;
using CarService.Domain.Entities;

namespace CarService.Application.Garages.Commands;

public sealed class CreateGarageHandler
{
    private readonly IGarageRepository _garages;

    public CreateGarageHandler(IGarageRepository garages)
    {
        _garages = garages;
    }

    public async Task<GarageDto> Handle(CreateGarageCommand cmd, CancellationToken ct = default)
    {
        var garage = new Garage(cmd.Name, cmd.City);

        await _garages.AddAsync(garage, ct);

        return new GarageDto(garage.Id, garage.Name, garage.City);
    }
}
