using Bogus;
using CarAuction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuction.Infrastructure.Persistence.EntityConfigurations;

public class TruckConfiguration : IEntityTypeConfiguration<Truck>
{
    public void Configure(EntityTypeBuilder<Truck> builder)
    {
        builder.HasData(GetSeedData());
    }

    private List<Truck> GetSeedData()
    {
        var trucks = new Faker<Truck>()
            .RuleFor(r => r.Manufacturer, f => f.Vehicle.Manufacturer())
            .RuleFor(r => r.Identifier, f => Guid.NewGuid().ToString())
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.Year, f => f.Random.Number(1950, 2024))
            .RuleFor(r => r.StartingBid, f => f.Random.Number(1000, 25000))
            .RuleFor(r => r.LoadCapacity, f => f.Random.Number(10000, 35000));

        return trucks.Generate(50);
    }
}
