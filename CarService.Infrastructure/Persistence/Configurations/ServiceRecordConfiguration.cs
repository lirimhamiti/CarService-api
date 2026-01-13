using CarService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class ServiceRecordConfiguration : IEntityTypeConfiguration<ServiceRecord>
{
    public void Configure(EntityTypeBuilder<ServiceRecord> b)
    {
        b.ToTable("ServiceRecords");
        b.HasKey(x => x.Id);

        b.Property(x => x.GarageId).IsRequired();
        b.Property(x => x.CarId).IsRequired();

        b.Property(x => x.ServiceDate).IsRequired();

        b.Property(x => x.Mileage).IsRequired();

        b.Property(x => x.Notes)
            .HasMaxLength(1000); 

        b.HasIndex(x => new { x.GarageId, x.CarId });
        b.HasIndex(x => x.ServiceDate);

        b.HasOne<Car>()
            .WithMany()
            .HasForeignKey(x => x.CarId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
