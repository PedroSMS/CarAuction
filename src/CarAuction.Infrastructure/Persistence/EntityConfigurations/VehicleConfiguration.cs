using CarAuction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuction.Infrastructure.Persistence.EntityConfigurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(k => k.Id);
        builder.Property(p => p.StartingBid).HasPrecision(14, 2);
        builder.HasIndex(p => p.Identifier).IsUnique();
        builder.UseTptMappingStrategy();
    }
}
