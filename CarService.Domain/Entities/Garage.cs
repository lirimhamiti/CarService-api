using CarService.Domain.Common;

namespace CarService.Domain.Entities;

public class Garage : AuditableEntity
{
    public string Name { get; private set; }
    public string City { get; private set; }

    private Garage() { } 

    public Garage(string name, string city)
    {
        Name = name;
        City = city;
    }
}
