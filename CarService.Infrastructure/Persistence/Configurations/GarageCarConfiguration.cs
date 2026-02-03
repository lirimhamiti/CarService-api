using CarService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class GarageCarConfiguration : IEntityTypeConfiguration<GarageCar>
{
    public void Configure(EntityTypeBuilder<GarageCar> b)
    {
        b.ToTable("GarageCars");
        b.HasKey(x => x.Id);

        b.Property(x => x.GarageId).IsRequired();
        b.Property(x => x.CarId).IsRequired();

        b.HasIndex(x => new { x.GarageId, x.CarId }).IsUnique();

        b.HasOne<Garage>()
            .WithMany()
            .HasForeignKey(x => x.GarageId)
            .OnDelete(DeleteBehavior.Cascade);

        b.HasOne<Car>()
            .WithMany()
            .HasForeignKey(x => x.CarId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
