using CarService.Application.Abstractions;
using CarService.Domain.Entities;
using CarService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Repositories;

public sealed class CarOwnerTokenRepository : ICarOwnerTokenRepository
{
    private readonly CarServiceDbContext _db;

    public CarOwnerTokenRepository(CarServiceDbContext db)
    {
        _db = db;
    }

    public async Task<CarOwnerToken?> GetActiveByHashAsync(string tokenHash, CancellationToken ct = default)
    {
        return await _db.CarOwnerTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TokenHash == tokenHash && x.IsActive, ct);
    }
}
