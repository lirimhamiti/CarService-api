using CarService.Application.Abstractions;
using CarService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarService.Infrastructure.Persistence.Repositories;

public sealed class ServiceRecordRepository : IServiceRecordRepository
{
    private readonly CarServiceDbContext _db;

    public ServiceRecordRepository(CarServiceDbContext db)
    {
        _db = db;
    }

    public async Task AddAsync(ServiceRecord record, CancellationToken ct = default)
    {
        _db.ServiceRecords.Add(record);
        await _db.SaveChangesAsync(ct);
    }

    public async Task<IReadOnlyList<ServiceRecord>> GetByCarAsync(
        Guid garageId,
        Guid carId,
        CancellationToken ct = default)
    {
        return await _db.ServiceRecords
            .Where(x => x.GarageId == garageId && x.CarId == carId)
            .OrderByDescending(x => x.ServiceDate)
            .ToListAsync(ct);
    }

    public async Task<IReadOnlyList<ServiceRecord>> GetByCarIdAsync(Guid carId, CancellationToken ct = default)
    {
        return await _db.ServiceRecords
            .AsNoTracking()
            .Where(x => x.CarId == carId)
            .OrderByDescending(x => x.ServiceDate)
            .ToListAsync(ct);
    }
}