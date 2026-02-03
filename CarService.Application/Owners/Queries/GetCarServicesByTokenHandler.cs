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
        var tokenHash = ExtractTokenHash(token);
        if (tokenHash is null)
            return null;

        var tokenRow = await _tokens.GetActiveByHashAsync(tokenHash, ct);
        if (tokenRow is null)
            return null;

        var list = await _services.GetOwnerByCarIdAsync(tokenRow.CarId, ct);

        return list;
    }

    private static string? ExtractTokenHash(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        var s = Uri.UnescapeDataString(input.Trim());

        var idx = s.IndexOf("token=", StringComparison.OrdinalIgnoreCase);
        if (idx >= 0)
        {
            var tok = s[(idx + "token=".Length)..];
            var amp = tok.IndexOf('&');
            if (amp >= 0) tok = tok[..amp];
            tok = tok.Trim();
            return tok.Length == 0 ? null : tok.ToUpperInvariant();
        }

        return s.ToUpperInvariant();
    }
}
