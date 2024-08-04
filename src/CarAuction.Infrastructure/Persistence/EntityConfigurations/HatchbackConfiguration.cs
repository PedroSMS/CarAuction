﻿using Bogus;
using CarAuction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarAuction.Infrastructure.Persistence.EntityConfigurations;

public class HatchbackConfiguration : IEntityTypeConfiguration<Hatchback>
{
    public void Configure(EntityTypeBuilder<Hatchback> builder)
    {
        builder.HasData(GetSeedData());
    }

    private List<Hatchback> GetSeedData()
    {
        var hatchBacks = new Faker<Hatchback>()
            .RuleFor(r => r.Manufacturer, f => f.Vehicle.Manufacturer())
            .RuleFor(r => r.Identifier, f => Guid.NewGuid().ToString())
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.Year, f => f.Random.Number(1950, 2024))
            .RuleFor(r => r.StartingBid, f => f.Random.Number(1000, 25000))
            .RuleFor(r => r.NumberOfDoors, f => f.Random.Number(1, 5));

        return hatchBacks.Generate(50);
    }
}
