namespace CarService.Application.Cars.Dtos;

public sealed record CarDto(
    Guid Id,
    string PlateNumber,
    string Vin,
    Guid GarageId,
    DateTime CreatedAt
);
