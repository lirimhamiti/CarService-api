using CarService.Application.Abstractions;
using CarService.Domain.Entities;
using CarService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.Repositories;

public sealed class GarageRepository : IGarageRepository
{
    private readonly CarServiceDbContext _context;

    public GarageRepository(CarServiceDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Garage garage, CancellationToken ct = default)
    {
        await _context.Garages.AddAsync(garage, ct);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<Garage?> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        return await _context.Garages.FirstOrDefaultAsync(x => x.Id == id, ct);
    }
}
