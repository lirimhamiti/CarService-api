using CarService.Application.Abstractions;
using CarService.Application.Owners.Dtos;
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

    public async Task<IReadOnlyList<OwnerServiceRecordDto>> GetOwnerByCarIdAsync(Guid carId, CancellationToken ct = default)
    {
        return await (
            from sr in _db.ServiceRecords.AsNoTracking()
            join g in _db.Garages.AsNoTracking() on sr.GarageId equals g.Id
            where sr.CarId == carId
            orderby sr.ServiceDate descending
            select new OwnerServiceRecordDto(
                sr.Id,
                sr.ServiceDate,
                sr.Mileage,
                sr.Notes,
                sr.CreatedAt,
                g.Id,
                g.Name,
                g.City
            )
        ).ToListAsync(ct);
    }
}