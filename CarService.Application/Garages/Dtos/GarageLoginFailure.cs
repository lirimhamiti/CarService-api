namespace CarService.Application.Garages.Dtos;

public enum GarageLoginFailure
{
    InvalidCredentials = 1,
    PendingApproval = 2,
    Rejected = 3
}

public sealed record GarageLoginFailureDto(
    GarageLoginFailure Code,
    string Message
);
