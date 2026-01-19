using CarService.Domain.Entities;

namespace CarService.Application.Abstractions;

public interface ICarOwnerTokenRepository
{
    Task AddAsync(CarOwnerToken token, CancellationToken ct = default);

    Task<CarOwnerToken?> GetActiveByCarIdAsync(Guid carId, CancellationToken ct = default);

    Task<CarOwnerToken?> GetActiveByHashAsync(string tokenHash, CancellationToken ct = default);

    Task DeactivateAllForCarAsync(Guid carId, CancellationToken ct = default);

    Task SaveChangesAsync(CancellationToken ct = default);

    string GenerateRawToken();
    string HashToken(string rawToken);
}
