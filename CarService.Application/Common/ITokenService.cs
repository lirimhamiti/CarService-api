namespace CarService.Application.Common;

public interface ITokenService
{
    (string RawToken, string TokenHash) GenerateOwnerToken();
}
