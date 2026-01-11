using CarService.Domain.Common;

namespace CarService.Domain.Entities;

public sealed class CarOwnerToken : AuditableEntity
{
    public Guid CarId { get; private set; }

    public Car? Car { get; private set; }

    public string TokenHash { get; private set; } = default!;
    public bool IsActive { get; private set; }

    private CarOwnerToken() { }

    public CarOwnerToken(Guid carId, string tokenHash)
    {
        if (carId == Guid.Empty) throw new ArgumentException("CarId cannot be empty.");
        if (string.IsNullOrWhiteSpace(tokenHash)) throw new ArgumentException("TokenHash is required.");

        Id = Guid.NewGuid(); 
        CarId = carId;
        TokenHash = tokenHash;
        IsActive = true;
    }


    public void Deactivate()
    {
        IsActive = false;
        MarkUpdated();
    }
}
