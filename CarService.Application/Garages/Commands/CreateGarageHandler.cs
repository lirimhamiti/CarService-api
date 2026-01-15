using CarService.Application.Abstractions;
using CarService.Application.Garages.Dtos;
using CarService.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CarService.Application.Garages.Commands;

public sealed class CreateGarageHandler
{
    private readonly IGarageRepository _garages;
    private readonly IPasswordHasher<Garage> _hasher;

    public CreateGarageHandler(IGarageRepository garages, IPasswordHasher<Garage> hasher)
    {
        _garages = garages;
        _hasher = hasher;
    }

    public async Task<GarageDto> Handle(CreateGarageCommand cmd, CancellationToken ct = default)
    {
        var username = cmd.Username.Trim();

            if (await _garages.UsernameExistsAsync(username, ct))
                throw new InvalidOperationException("USERNAME_TAKEN");

        var garage = new Garage(cmd.Name, cmd.City, cmd.Email, cmd.Username, passwordHash: "temp");
        var hash = _hasher.HashPassword(garage, cmd.Password);
        garage.SetPasswordHash(hash);

        await _garages.AddAsync(garage, ct);

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
