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

    public async Task<(GarageLoginResultDto? result, GarageLoginFailureDto? failure)> Handle(
      GarageLoginCommand cmd,
      CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(cmd.Username) || string.IsNullOrWhiteSpace(cmd.Password))
        {
            return (null, new GarageLoginFailureDto(
                GarageLoginFailure.InvalidCredentials,
                "Invalid username or password."
            ));
        }

        var username = cmd.Username.Trim();
        var garage = await _garages.GetByUsernameAsync(username, ct);

        if (garage is null)
        {
            return (null, new GarageLoginFailureDto(
                GarageLoginFailure.InvalidCredentials,
                "Invalid username or password."
            ));
        }

        var verify = _hasher.VerifyHashedPassword(garage, garage.PasswordHash, cmd.Password);
        if (verify == PasswordVerificationResult.Failed)
        {
            return (null, new GarageLoginFailureDto(
                GarageLoginFailure.InvalidCredentials,
                "Invalid username or password."
            ));
        }

        if (garage.Status == GarageStatus.Pending)
        {
            return (null, new GarageLoginFailureDto(
                GarageLoginFailure.PendingApproval,
                "Your account is pending admin approval."
            ));
        }

        if (garage.Status == GarageStatus.Rejected)
        {
            return (null, new GarageLoginFailureDto(
                GarageLoginFailure.Rejected,
                "Your account was rejected. Please contact support."
            ));
        }

        if (garage.Status != GarageStatus.Approved)
        {
            return (null, new GarageLoginFailureDto(
                GarageLoginFailure.InvalidCredentials,
                "Login is not allowed for this account."
            ));
        }

        return (new GarageLoginResultDto(
            garage.Id,
            garage.Name,
            garage.City,
            garage.Username
        ), null);
    }

}
