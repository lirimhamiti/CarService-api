namespace CarService.Application.Cars.Dtos;

public sealed record CarDto(
    Guid Id,
    string PlateNumber,
    string Vin,
    DateTime CreatedAt
);
