using CarService.Application.Abstractions;
using CarService.Domain.Entities;
using CarService.Domain.Enums;
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

    public async Task<IReadOnlyList<Garage>> GetAllAsync(CancellationToken ct = default)
    {
        return await _context.Garages
            .AsNoTracking()
            .ToListAsync(ct);
    }

    public async Task<Garage?> GetByUsernameAsync(string username, CancellationToken ct = default)
    {
        var u = username.Trim();
        return await _context.Garages.FirstOrDefaultAsync(x => x.Username == u, ct);
    }

    public async Task<List<Garage>> GetPendingAsync(CancellationToken ct = default)
       => await _context.Garages
           .AsNoTracking()
           .Where(x => x.Status == GarageStatus.Pending)
           .OrderBy(x => x.CreatedAt)
           .ToListAsync(ct);

    public Task SaveChangesAsync(CancellationToken ct = default)
        => _context.SaveChangesAsync(ct);
}
