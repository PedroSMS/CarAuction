using CarAuction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuction.Infrastructure.Persistence.EntityConfigurations;

public class BidConfiguration : IEntityTypeConfiguration<Bid>
{
    public void Configure(EntityTypeBuilder<Bid> builder)
    {
        builder.Property(p => p.Value)
            .HasPrecision(14, 2);
        builder.HasIndex(i => new { i.AuctionId, i.Value })
            .IsUnique();
    }
}
