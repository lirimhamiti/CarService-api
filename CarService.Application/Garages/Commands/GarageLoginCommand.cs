namespace CarService.Application.Garages.Commands;

public sealed record GarageLoginCommand(
    string Username,
    string Password
);
