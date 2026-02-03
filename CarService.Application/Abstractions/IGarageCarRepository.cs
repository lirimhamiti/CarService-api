using CarService.Domain.Entities;

namespace CarService.Application.Abstractions;

public interface IGarageCarRepository
{
    Task<bool> ExistsAsync(Guid garageId, Guid carId, CancellationToken ct = default);
    Task AddAsync(GarageCar link, CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
