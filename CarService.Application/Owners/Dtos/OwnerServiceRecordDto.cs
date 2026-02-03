namespace CarService.Application.Owners.Dtos;

public sealed record OwnerServiceRecordDto(
    Guid Id,
    DateTime ServiceDate,
    int Mileage,
    string? Notes,
    DateTime CreatedAt,
    Guid GarageId,
    string GarageName,
    string GarageCity
);

