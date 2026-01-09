using CarService.Domain.Common;

namespace CarService.Domain.Entities;

public sealed class Garage : AuditableEntity
{
    public string Name { get; private set; } = null!;
    public string City { get; private set; } = null!;

    private Garage() { }

    public Garage(string name, string city)
    {
        SetName(name);
        SetCity(city);
    }

    public void Update(string name, string city)
    {
        SetName(name);
        SetCity(city);
    }

    private void SetName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Garage name is required.");

        Name = name.Trim();
    }

    private void SetCity(string city)
    {
        if (string.IsNullOrWhiteSpace(city))
            throw new ArgumentException("City is required.");

        City = city.Trim();
    }
}
