using CarAuction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuction.Infrastructure.Persistence.EntityConfigurations;

public class SedanConfiguration : IEntityTypeConfiguration<Sedan>
{
    public void Configure(EntityTypeBuilder<Sedan> builder)
    {
        builder.ToTable(nameof(Sedan), SchemaNames.Auction);
    }
}
