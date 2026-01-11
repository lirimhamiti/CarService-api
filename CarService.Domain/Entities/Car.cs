using CarService.Domain.Common;

namespace CarService.Domain.Entities;

public sealed class Car : AuditableEntity
{
    public Guid GarageId { get; private set; }
    public string Vin { get; private set; } = default!;
    public string PlateNumber { get; private set; } = default!;

    private Car() { }

    public Car(Guid garageId, string plateNumber, string vin)
    {
        if (garageId == Guid.Empty) throw new ArgumentException("GarageId cannot be empty.");
        if (string.IsNullOrWhiteSpace(plateNumber)) throw new ArgumentException("PlateNumber is required.");

        Id = Guid.NewGuid();
        GarageId = garageId;
        PlateNumber = plateNumber.Trim().ToUpperInvariant();
        Vin = vin?.Trim();
    }

    public void ChangeGarage(Guid newGarageId)
    {
        if (newGarageId == Guid.Empty) throw new ArgumentException("GarageId cannot be empty.");
        GarageId = newGarageId;
        MarkUpdated();
    }
}
