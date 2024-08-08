using CarAuction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuction.Infrastructure.Persistence.EntityConfigurations;

public class HatchbackConfiguration : IEntityTypeConfiguration<Hatchback>
{
    public void Configure(EntityTypeBuilder<Hatchback> builder)
    {
        builder.ToTable(nameof(Hatchback), SchemaNames.Auction);
    }
}
