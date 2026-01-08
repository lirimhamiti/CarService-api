using CarService.Domain.Common;

namespace CarService.Domain.Entities;

public class Car : AuditableEntity
{
    public string VINnumber { get; private set; }
    public string Brand { get; private set; }
    public string Model { get; private set; }
    public int Year { get; private set; }

    public Guid GarageId { get; private set; }
    public Garage Garage { get; private set; }

    private Car() { } 

    public Car(
        string plateNumber,
        string brand,
        string model,
        int year,
        Guid garageId)
    {
        VINnumber = plateNumber;
        Brand = brand;
        Model = model;
        Year = year;
        GarageId = garageId;
    }
}
