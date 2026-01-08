using CarService.Domain.Common;

namespace CarService.Domain.Entities;

public class ServiceRecord : AuditableEntity
{
    public Guid CarId { get; private set; }
    public Car Car { get; private set; }

    public DateTime ServiceDate { get; private set; }
    public int Mileage { get; private set; }
    public string Description { get; private set; }

    private ServiceRecord() { } 

    public ServiceRecord(
        Guid carId,
        DateTime serviceDate,
        int mileage,
        string description)
    {
        CarId = carId;
        ServiceDate = serviceDate;
        Mileage = mileage;
        Description = description;
    }
}
