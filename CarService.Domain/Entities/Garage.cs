using CarService.Domain.Common;
using CarService.Domain.Enums;

namespace CarService.Domain.Entities;

public sealed class Garage : AuditableEntity 
{
    public string Name { get; private set; } = default!;
    public string City { get; private set; } = default!;

    public string Email { get; private set; } = default!;
    public string Username { get; private set; } = default!;
    public string PasswordHash { get; private set; } = default!;
    public GarageStatus Status { get; private set; } = GarageStatus.Pending;
    public DateTime? ApprovedAt { get; private set; }


    private Garage() { }

    public Garage(string name, string city, string email, string username, string passwordHash)
    {
        Id = Guid.NewGuid();
        Name = name;
        City = city;
        Email = email;
        Username = username;
        PasswordHash = passwordHash;
        Status = GarageStatus.Pending;
        ApprovedAt = null;
    }

    public void Approve()
    {
        Status = GarageStatus.Approved;
        ApprovedAt = DateTime.UtcNow;
        MarkUpdated();
    }

    public void Reject()
    {
        Status = GarageStatus.Rejected;
        MarkUpdated();
    }

    public void SetPasswordHash(string passwordHash)
    {
        if (string.IsNullOrWhiteSpace(passwordHash))
            throw new ArgumentException("PasswordHash is required.");

        PasswordHash = passwordHash;
        MarkUpdated();
    }


}
