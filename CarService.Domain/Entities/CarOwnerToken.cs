using CarService.Domain.Common;

namespace CarService.Domain.Entities;

public class CarOwnerToken : AuditableEntity
{
    public Guid CarId { get; private set; }
    public Car Car { get; private set; }

    public string TokenHash { get; private set; }
    public bool IsActive { get; private set; }

    private CarOwnerToken() { } 

    public CarOwnerToken(Guid carId, string tokenHash)
    {
        if (carId == Guid.Empty) throw new ArgumentException("CarId cannot be empty.");
        if (string.IsNullOrWhiteSpace(tokenHash)) throw new ArgumentException("TokenHash is required.");

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
