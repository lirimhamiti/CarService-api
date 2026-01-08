using CarService.Domain.Common;
using CarService.Domain.ValueObjects;

namespace CarService.Domain.Entities;

public class Car : AuditableEntity
{
    public PlateNumber PlateNumber { get; private set; }
    public string? Vin { get; private set; } 
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public int Year { get; private set; }

    public Guid GarageId { get; private set; }
    public Garage Garage { get; private set; }

    private Car() { } 

    public Car(
        PlateNumber plateNumber,
        string? vin,
        string brand,
        string model,
        int year,
        Guid garageId)
    {
        if (garageId == Guid.Empty)
            throw new ArgumentException("GarageId cannot be empty.");

        PlateNumber = plateNumber ?? throw new ArgumentNullException(nameof(plateNumber));
        Vin = string.IsNullOrWhiteSpace(vin) ? null : vin.Trim().ToUpperInvariant();

        if (string.IsNullOrWhiteSpace(brand))
            throw new ArgumentException("Brand is required.");

        if (string.IsNullOrWhiteSpace(model))
            throw new ArgumentException("Model is required.");

        if (year < 1950 || year > DateTime.UtcNow.Year + 1)
            throw new ArgumentException("Year is invalid.");

        Brand = brand.Trim();
        Model = model.Trim();
        Year = year;
        GarageId = garageId;
    }
}
