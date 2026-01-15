using System.Security.Cryptography;
using System.Text;
using CarService.Application.Abstractions;
using CarService.Application.Owners.Dtos;

namespace CarService.Application.Owners.Queries;

public sealed class GetCarServicesByTokenHandler
{
    private readonly ICarOwnerTokenRepository _tokens;
    private readonly IServiceRecordRepository _services;

    public GetCarServicesByTokenHandler(
        ICarOwnerTokenRepository tokens,
        IServiceRecordRepository services)
    {
        _tokens = tokens;
        _services = services;
    }

    public async Task<IReadOnlyList<OwnerServiceRecordDto>?> Handle(string token, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(token))
            return null;

        var tokenHash = HashToken(token);

        var tokenRow = await _tokens.GetActiveByHashAsync(tokenHash, ct);
        if (tokenRow is null)
            return null;

        var records = await _services.GetByCarIdAsync(tokenRow.CarId, ct);

        return records
            .OrderByDescending(x => x.ServiceDate)
            .Select(x => new OwnerServiceRecordDto(
                x.Id,
                x.ServiceDate,
                x.Mileage,
                x.Notes,
                x.CreatedAt
            ))
            .ToList();
    }

    private static string HashToken(string raw)
    {
        var bytes = Encoding.UTF8.GetBytes(raw.Trim());
        var hash = SHA256.HashData(bytes);
        return Convert.ToHexString(hash);
    }
}
