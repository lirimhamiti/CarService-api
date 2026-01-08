using CarService.Domain.Common;

namespace CarService.Domain.ValueObjects;

public sealed class PlateNumber : ValueObject
{
    public string Value { get; }

    private PlateNumber(string value) => Value = value;

    public static PlateNumber Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Plate number is required.");

        value = value.Trim().ToUpperInvariant();

        if (value.Length < 7 || value.Length > 8)
            throw new ArgumentException("Plate number length is invalid.");

        return new PlateNumber(value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString() => Value;
}
