namespace CarService.Application.Cars.Dtos;

public sealed record CarDto(Guid Id, string PlateNumber, string? Vin, string Brand, string Model, int Year, Guid GarageId);
