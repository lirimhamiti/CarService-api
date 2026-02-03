using CarService.Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace CarService.Infrastructure.Persistence;

public sealed class CarServiceDbContext : DbContext
{
    public CarServiceDbContext(DbContextOptions<CarServiceDbContext> options) : base(options) { }

    public DbSet<Garage> Garages => Set<Garage>();
    public DbSet<Car> Cars => Set<Car>();
    public DbSet<ServiceRecord> ServiceRecords => Set<ServiceRecord>();
    public DbSet<CarOwnerToken> CarOwnerTokens => Set<CarOwnerToken>();
    public DbSet<GarageCar> GarageCars => Set<GarageCar>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CarServiceDbContext).Assembly);
    }


}
