using CarService.Application.Abstractions;
using CarService.Domain.Entities;
using CarService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.Repositories;

public sealed class CarRepository : ICarRepository
{
    private readonly CarServiceDbContext _context;

    public CarRepository(CarServiceDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Car car, CancellationToken ct = default)
    {
        await _context.Cars.AddAsync(car, ct);
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
    {
        return _context.SaveChangesAsync(ct);
    }


    public async Task<Car?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Cars.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<bool> VinExistsAsync(string vin, CancellationToken ct = default)
    {
        return await _context.Cars.AnyAsync(x => x.Vin == vin, ct);
    }

    public Task<bool> PlateExistsAsync(string plateNumber, CancellationToken ct = default)
    {
        return _context.Cars.AnyAsync(x => x.PlateNumber == plateNumber, ct);
    }

    public async Task<IReadOnlyList<Car>> GetByGarageIdAsync(Guid garageId, CancellationToken ct = default)
    {
        return await _context.GarageCars
            .AsNoTracking()
            .Where(gc => gc.GarageId == garageId)
            .Join(
                _context.Cars.AsNoTracking(),
                gc => gc.CarId,
                c => c.Id,
                (gc, c) => c
            )
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync(ct);
    }


    public Task<Car?> GetByVinAsync(string vin, CancellationToken ct = default)
        => _context.Cars.AsNoTracking().FirstOrDefaultAsync(x => x.Vin == vin, ct);

    public async Task<CarOwnerToken?> GetActiveByCarIdAsync(Guid carId, CancellationToken ct = default)
    {
        return await _context.CarOwnerTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CarId == carId && x.IsActive, ct);
    }



}
