using CarService.Domain.Entities;

namespace CarService.Application.Abstractions;

public interface ICarRepository
{
    Task AddAsync(Car car, CancellationToken ct = default);
    Task<Car?> GetByIdAsync(Guid id, CancellationToken ct = default);

    Task<bool> VinExistsAsync(string vin, CancellationToken ct = default);
}
