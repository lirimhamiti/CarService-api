using CarService.Domain.Common;

namespace CarService.Domain.Entities;

public sealed class GarageCar : AuditableEntity
{
    public Guid GarageId { get; private set; }
    public Guid CarId { get; private set; }

    private GarageCar() { }

    public GarageCar(Guid garageId, Guid carId)
    {
        if (garageId == Guid.Empty) throw new ArgumentException("GarageId cannot be empty.");
        if (carId == Guid.Empty) throw new ArgumentException("CarId cannot be empty.");

        Id = Guid.NewGuid();
        GarageId = garageId;
        CarId = carId;
    }
}
