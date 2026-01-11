using CarService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarService.Infrastructure.Persistence.Configurations;

public sealed class GarageConfiguration : IEntityTypeConfiguration<Garage>
{
    public void Configure(EntityTypeBuilder<Garage> b)
    {
        b.ToTable("Garages");

        b.HasKey(x => x.Id);

        b.Property(x => x.Name).IsRequired();
        b.Property(x => x.City).IsRequired();

        b.Property(x => x.Email).IsRequired().HasMaxLength(256);
        b.Property(x => x.Username).IsRequired().HasMaxLength(64);
        b.Property(x => x.PasswordHash).IsRequired().HasMaxLength(512);

        b.Property(x => x.Status)
            .IsRequired()
            .HasConversion<int>();

        b.Property(x => x.ApprovedAt).IsRequired(false);

        b.HasIndex(x => x.Username).IsUnique();
        b.HasIndex(x => x.Email).IsUnique();
    }
}
