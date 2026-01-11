namespace CarService.Application.Garages.Dtos;

public sealed record GarageLoginResultDto(
    Guid GarageId,
    string Name,
    string City,
    string Username
);
