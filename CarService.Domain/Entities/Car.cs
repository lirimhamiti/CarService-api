using CarService.Domain.Common;

namespace CarService.Domain.Entities;

public sealed class Car : AuditableEntity
{
    public string Vin { get; private set; } = default!;
    public string PlateNumber { get; private set; } = default!;

    private Car() { }

    public Car(string plateNumber, string vin)
    {
        if (string.IsNullOrWhiteSpace(plateNumber)) throw new ArgumentException("PlateNumber is required.");
        if (string.IsNullOrWhiteSpace(vin)) throw new ArgumentException("VIN is required.");

        var v = vin.Trim().ToUpperInvariant();
        if (v.Length != 17) throw new ArgumentException("VIN must be exactly 17 characters.");

        Id = Guid.NewGuid();
        PlateNumber = plateNumber.Trim().ToUpperInvariant();
        Vin = v;
    }

    public void UpdatePlate(string plateNumber)
    {
        if (string.IsNullOrWhiteSpace(plateNumber)) throw new ArgumentException("PlateNumber is required.");
        PlateNumber = plateNumber.Trim().ToUpperInvariant();
        MarkUpdated();
    }
}
