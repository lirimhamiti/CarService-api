using CarService.Domain.Common;

namespace CarService.Domain.ValueObjects;

public sealed class Mileage : ValueObject
{
    public int Km { get; }

    private Mileage(int km) => Km = km;

    public static Mileage Create(int km)
    {
        if (km < 0) throw new ArgumentException("Mileage cannot be negative.");
        return new Mileage(km);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Km;
    }

    public override string ToString() => $"{Km} km";
}
