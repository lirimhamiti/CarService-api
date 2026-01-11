namespace CarService.Application.Owners.Dtos;

public sealed record OwnerCarDto(
    Guid CarId,
    string PlateNumber,
    string Vin,
    Guid CurrentGarageId
);
