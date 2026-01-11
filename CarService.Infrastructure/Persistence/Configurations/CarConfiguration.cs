using CarService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class CarConfiguration : IEntityTypeConfiguration<Car>
{
    public void Configure(EntityTypeBuilder<Car> b)
    {
        b.ToTable("Cars");
        b.HasKey(x => x.Id);

        b.Property(x => x.PlateNumber).HasMaxLength(8).IsRequired();

        // Relationship WITHOUT navigation property
        b.HasOne<Garage>()
            .WithMany()
            .HasForeignKey(x => x.GarageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
