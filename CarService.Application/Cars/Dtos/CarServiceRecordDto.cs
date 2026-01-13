namespace CarService.Application.Cars.Dtos;

public sealed record CreateServiceRecordDto(
    DateTime ServiceDate,
    int Mileage,
    string? Notes
);
