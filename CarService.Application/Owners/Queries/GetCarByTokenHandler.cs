using System.Security.Cryptography;
using System.Text;
using CarService.Application.Abstractions;
using CarService.Application.Owners.Dtos;

namespace CarService.Application.Owners.Queries;

public sealed class GetCarByTokenHandler
{
    private readonly ICarOwnerTokenRepository _tokens;
    private readonly ICarRepository _cars;

    public GetCarByTokenHandler(ICarOwnerTokenRepository tokens, ICarRepository cars)
    {
        _tokens = tokens;
        _cars = cars;
    }

    public async Task<OwnerCarDto?> Handle(string token, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(token))
            return null;

        var tokenHash = HashToken(token);

        var tokenRow = await _tokens.GetActiveByHashAsync(tokenHash, ct);
        if (tokenRow is null)
            return null;

        var car = await _cars.GetByIdAsync(tokenRow.CarId, ct);
        if (car is null)
            return null;

        return new OwnerCarDto(
            car.Id,
            car.PlateNumber,
            car.Vin,
            car.GarageId
        );
    }

    private static string HashToken(string raw)
    {
        var bytes = Encoding.UTF8.GetBytes(raw.Trim());
        var hash = SHA256.HashData(bytes);
        return Convert.ToHexString(hash); 
    }
}
