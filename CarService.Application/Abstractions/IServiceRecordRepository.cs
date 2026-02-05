using CarService.Application.Owners.Dtos;
using CarService.Domain.Entities;

namespace CarService.Application.Abstractions;

public interface IServiceRecordRepository
{
    Task AddAsync(ServiceRecord record, CancellationToken ct = default);
    Task<IReadOnlyList<ServiceRecord>> GetByCarAsync(
        Guid garageId,
        Guid carId,
        CancellationToken ct = default
    );

    Task<IReadOnlyList<ServiceRecord>> GetByCarIdAsync(Guid carId, CancellationToken ct = default);

    Task<IReadOnlyList<OwnerServiceRecordDto>> GetOwnerByCarIdAsync(Guid carId, CancellationToken ct = default);

    Task<int> GetMaxMileageByCarIdAsync(Guid carId, CancellationToken ct = default);



}
