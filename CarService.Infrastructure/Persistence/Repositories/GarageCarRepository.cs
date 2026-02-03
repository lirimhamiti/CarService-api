using CarService.Application.Abstractions;
using CarService.Domain.Entities;
using CarService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Repositories;

public sealed class GarageCarRepository : IGarageCarRepository
{
    private readonly CarServiceDbContext _db;

    public GarageCarRepository(CarServiceDbContext db) => _db = db;

    public Task<bool> ExistsAsync(Guid garageId, Guid carId, CancellationToken ct = default) =>
        _db.GarageCars.AsNoTracking().AnyAsync(x => x.GarageId == garageId && x.CarId == carId, ct);

    public Task AddAsync(GarageCar link, CancellationToken ct = default) =>
        _db.GarageCars.AddAsync(link, ct).AsTask();

    public Task SaveChangesAsync(CancellationToken ct = default) =>
        _db.SaveChangesAsync(ct);
}
