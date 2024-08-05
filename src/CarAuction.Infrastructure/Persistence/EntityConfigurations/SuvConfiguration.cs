using Bogus;
using CarAuction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuction.Infrastructure.Persistence.EntityConfigurations;

public class SuvConfiguration : IEntityTypeConfiguration<Suv>
{
    public void Configure(EntityTypeBuilder<Suv> builder)
    {
        //builder.HasData(GetSeedData());
    }

    private List<Suv> GetSeedData()
    {
        var suvs = new Faker<Suv>()
            .RuleFor(r => r.Manufacturer, f => f.Vehicle.Manufacturer())
            .RuleFor(r => r.Identifier, f => Guid.NewGuid().ToString())
            .RuleFor(r => r.Model, f => f.Vehicle.Model())
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.Year, f => f.Random.Number(1950, 2024))
            .RuleFor(r => r.StartingBid, f => f.Random.Number(1000, 25000))
            .RuleFor(r => r.NumberOfSeats, f => f.Random.Number(5, 9));

        return suvs.Generate(10);
    }
}
