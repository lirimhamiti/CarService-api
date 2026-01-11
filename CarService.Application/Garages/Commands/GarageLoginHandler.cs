using CarService.Application.Abstractions;
using CarService.Application.Garages.Dtos;
using CarService.Domain.Enums;
using Microsoft.AspNetCore.Identity;

namespace CarService.Application.Garages.Commands;

public sealed class GarageLoginHandler
{
    private readonly IGarageRepository _garages;
    private readonly IPasswordHasher<CarService.Domain.Entities.Garage> _hasher;

    public GarageLoginHandler(
        IGarageRepository garages,
        IPasswordHasher<CarService.Domain.Entities.Garage> hasher)
    {
        _garages = garages;
        _hasher = hasher;
    }

    public async Task<GarageLoginResultDto?> Handle(GarageLoginCommand cmd, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(cmd.Username) || string.IsNullOrWhiteSpace(cmd.Password))
            return null;

        var garage = await _garages.GetByUsernameAsync(cmd.Username, ct);
        if (garage is null)
            return null;

        if (garage.Status != GarageStatus.Approved)
            return null;

        var verify = _hasher.VerifyHashedPassword(garage, garage.PasswordHash, cmd.Password);

        if (verify == PasswordVerificationResult.Failed)
            return null;

        return new GarageLoginResultDto(
            garage.Id,
            garage.Name,
            garage.City,
            garage.Username
        );
    }
}
