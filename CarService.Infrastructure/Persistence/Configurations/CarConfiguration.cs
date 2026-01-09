using CarService.Domain.Entities;
using CarService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarService.Infrastructure.Persistence.Configurations;

public sealed class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> builder)
    {
        builder.Property(x => x.PlateNumber)
            .HasConversion(
                v => v.Value,
                v => PlateNumber.Create(v))
            .HasMaxLength(8)
            .IsRequired();
    }
}
