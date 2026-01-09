using CarService.Application.Abstractions;
using CarService.Application.Cars.Commands;
using CarService.Application.Garages.Commands;
using CarService.Infrastructure.Persistence;
using CarService.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarService.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddPersistence(configuration);
        return services;
    }

    private static IServiceCollection AddPersistence(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<CarServiceDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IGarageRepository, GarageRepository>();
        services.AddScoped<CreateGarageHandler>();

        services.AddScoped<IGarageRepository, GarageRepository>();
        services.AddScoped<ICarRepository, CarRepository>();

        services.AddScoped<GetGaragesHandler>();
        services.AddScoped<GetGarageByIdHandler>();
        services.AddScoped<CreateCarHandler>();



        return services;
    }
}
