namespace CarService.Application.Cars.Commands;

public sealed record CreateCarCommand(
    Guid GarageId,
    string PlateNumber,
    string? Vin,
    string Brand,
    string Model,
    int Year);
