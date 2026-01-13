namespace CarService.Application.Cars.Dtos;

public sealed record ServiceRecordDto(
    Guid Id,
    Guid CarId,
    Guid GarageId,
    DateTime ServiceDate,
    int Mileage,
    string? Notes,
    DateTime CreatedAtUtc
);
