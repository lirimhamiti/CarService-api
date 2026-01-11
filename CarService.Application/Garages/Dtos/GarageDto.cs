namespace CarService.Application.Garages.Dtos;

public sealed record GarageDto(
    Guid Id,
    string Name,
    string City,
    string Email,
    string Username,
    string Status
);
