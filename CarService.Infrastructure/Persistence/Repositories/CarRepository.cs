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
        await _context.SaveChangesAsync(ct);
    }

    public async Task<Car?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Cars.FirstOrDefaultAsync(x => x.Id == id, ct);
    }

    public async Task<bool> VinExistsAsync(string vin, CancellationToken ct = default)
    {
        return await _context.Cars.AnyAsync(x => x.Vin == vin, ct);
    }
}
