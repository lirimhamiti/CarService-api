using CarService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class CarOwnerTokenConfiguration : IEntityTypeConfiguration<CarOwnerToken>
{
    public void Configure(EntityTypeBuilder<CarOwnerToken> b)
    {
        b.ToTable("CarOwnerTokens");
        b.HasKey(x => x.Id);

        b.HasIndex(x => x.CarId).IsUnique();

        b.HasOne(x => x.Car)
                 .WithOne()
                 .HasForeignKey<CarOwnerToken>(x => x.CarId)
                 .OnDelete(DeleteBehavior.Cascade);
    }
}
