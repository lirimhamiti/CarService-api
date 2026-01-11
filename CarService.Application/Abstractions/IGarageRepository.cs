using CarService.Domain.Entities;

namespace CarService.Application.Abstractions;

public interface IGarageRepository
{
    Task AddAsync(Garage garage, CancellationToken ct = default);
    Task<Garage?> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<Garage?> GetByUsernameAsync(string username, CancellationToken ct = default);
    Task<IReadOnlyList<Garage>> GetAllAsync(CancellationToken ct = default);

    Task<List<Garage>> GetPendingAsync(CancellationToken ct = default);
    Task SaveChangesAsync(CancellationToken ct = default);
}
