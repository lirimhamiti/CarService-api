using CarService.Domain.Entities;

namespace CarService.Application.Abstractions;

public interface ICarRepository
{
    Task AddAsync(Car car, CancellationToken ct = default);

    Task SaveChangesAsync( CancellationToken ct = default);

    Task<Car?> GetByIdAsync(Guid id, CancellationToken ct = default);

    Task<bool> VinExistsAsync(string vin, CancellationToken ct = default);

    Task<bool> PlateExistsAsync(string plateNumber, CancellationToken ct = default);

    Task<IReadOnlyList<Car>> GetByGarageIdAsync(Guid garageId, CancellationToken ct = default);


}
