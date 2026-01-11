using CarService.Domain.Common;

namespace CarService.Domain.Entities;

public sealed class ServiceRecord : AuditableEntity
{
    public Guid CarId { get; private set; }
    public Guid GarageId { get; private set; }
    public DateTime ServiceDate { get; private set; }
    public int Mileage { get; private set; }
    public string? Notes { get; private set; }

    private ServiceRecord() { }

    public ServiceRecord(Guid carId, Guid garageId, DateTime serviceDate, int mileage, string? notes)
    {
        if (carId == Guid.Empty) throw new ArgumentException("CarId cannot be empty.");
        if (garageId == Guid.Empty) throw new ArgumentException("GarageId cannot be empty.");
        if (mileage < 0) throw new ArgumentOutOfRangeException(nameof(mileage));

        Id = Guid.NewGuid();
        CarId = carId;
        GarageId = garageId;
        ServiceDate = serviceDate;
        Mileage = mileage;
        Notes = notes;
    }
}
