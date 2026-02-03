using CarService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> b)
    {
        b.ToTable("Cars");
        b.HasKey(x => x.Id);

        b.Property(x => x.PlateNumber)
            .HasMaxLength(12)
            .IsRequired();

        b.Property(x => x.Vin)
            .HasMaxLength(17)
            .IsRequired()
            .IsFixedLength();

        b.HasIndex(x => x.Vin).IsUnique();

    }
}
