using CarService.Domain.Entities;

namespace CarService.Application.Abstractions;

public interface ICarOwnerTokenRepository
{
    //Task AddAsync(CarOwnerToken token, CancellationToken ct = default);
    //Task<CarOwnerToken?> GetActiveByCarIdAsync(Guid carId, CancellationToken ct = default);
    Task<CarOwnerToken?> GetActiveByHashAsync(string tokenHash, CancellationToken ct = default);

}
