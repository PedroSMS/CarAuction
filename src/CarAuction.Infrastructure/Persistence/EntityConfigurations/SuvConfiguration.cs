using CarAuction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuction.Infrastructure.Persistence.EntityConfigurations;

public class SuvConfiguration : IEntityTypeConfiguration<Suv>
{
    public void Configure(EntityTypeBuilder<Suv> builder)
    {
        builder.ToTable(nameof(Suv), SchemaNames.Auction);
    }
}
