using CarService.Application.Abstractions;
using CarService.Domain.Entities;
using CarService.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CarService.Infrastructure.Repositories;

public sealed class CarOwnerTokenRepository : ICarOwnerTokenRepository
{
    private readonly CarServiceDbContext _db;

    public CarOwnerTokenRepository(CarServiceDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(CarOwnerToken token, CancellationToken ct = default)
    {
        await _db.CarOwnerTokens.AddAsync(token, ct);
    }

    public async Task<CarOwnerToken?> GetActiveByCarIdAsync(Guid carId, CancellationToken ct = default)
    {
        return await _db.CarOwnerTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.CarId == carId && x.IsActive, ct);
    }

    public async Task<CarOwnerToken?> GetActiveByHashAsync(string tokenHash, CancellationToken ct = default)
    {
        return await _db.CarOwnerTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.TokenHash == tokenHash && x.IsActive, ct);
    }

    public async Task DeactivateAllForCarAsync(Guid carId, CancellationToken ct = default)
    {
        var activeTokens = await _db.CarOwnerTokens
            .Where(x => x.CarId == carId && x.IsActive)
            .ToListAsync(ct);

        foreach (var t in activeTokens)
        {
            t.Deactivate(); 
        }
    }

    public Task SaveChangesAsync(CancellationToken ct = default)
        => _db.SaveChangesAsync(ct);

    public string GenerateRawToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(32); // 256-bit
        var base64 = Convert.ToBase64String(bytes);

        return base64.Replace("+", "-").Replace("/", "_").TrimEnd('=');
    }

    public string HashToken(string rawToken)
    {
        if (string.IsNullOrWhiteSpace(rawToken))
            throw new ArgumentException("Token cannot be empty.", nameof(rawToken));

        var bytes = Encoding.UTF8.GetBytes(rawToken);
        var hash = SHA256.HashData(bytes);

        return Convert.ToHexString(hash); 
    }

}
