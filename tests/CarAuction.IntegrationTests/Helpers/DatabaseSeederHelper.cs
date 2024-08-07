using Bogus;
using CarAuction.Domain.Entities;

namespace CarAuction.IntegrationTests.Helpers;

public static class DatabaseSeederHelper
{
    public static List<Hatchback> GetHatchbacks(int number)
    {
        return new Faker<Hatchback>()
            .RuleFor(r => r.Manufacturer, f => f.Vehicle.Manufacturer())
            .RuleFor(r => r.Identifier, f => Guid.NewGuid().ToString())
            .RuleFor(r => r.Model, f => f.Vehicle.Model())
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.Year, f => f.Random.Number(1950, 2024))
            .RuleFor(r => r.StartingBid, f => f.Random.Number(1000, 25000))
            .RuleFor(r => r.NumberOfDoors, f => f.Random.Number(1, 5))
            .Generate(number);
    }

    public static List<Sedan> GetSedans(int number)
    {
        return new Faker<Sedan>()
            .RuleFor(r => r.Manufacturer, f => f.Vehicle.Manufacturer())
            .RuleFor(r => r.Identifier, f => Guid.NewGuid().ToString())
            .RuleFor(r => r.Model, f => f.Vehicle.Model())
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.Year, f => f.Random.Number(1950, 2024))
            .RuleFor(r => r.StartingBid, f => f.Random.Number(1000, 25000))
            .RuleFor(r => r.NumberOfDoors, f => f.Random.Number(1, 5))
            .Generate(number);
    }

    public static List<Truck> GetTrucks(int number)
    {
        return new Faker<Truck>()
            .RuleFor(r => r.Manufacturer, f => f.Vehicle.Manufacturer())
            .RuleFor(r => r.Identifier, f => Guid.NewGuid().ToString())
            .RuleFor(r => r.Model, f => f.Vehicle.Model())
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.Year, f => f.Random.Number(1950, 2024))
            .RuleFor(r => r.StartingBid, f => f.Random.Number(1000, 25000))
            .RuleFor(r => r.LoadCapacity, f => f.Random.Number(10000, 45000))
            .Generate(number);
    }

    public static List<Auction> GetAuctionsForCar(int number, Guid carId)
    {
        return new Faker<Auction>()
            .RuleFor(r => r.Id, f => Guid.NewGuid())
            .RuleFor(r => r.CarId, f => carId)
            .RuleFor(r => r.Bids, f => 
            [
                new() { Value = 25500, PlacedAtUtc = DateTime.UtcNow }
            ])
            .Generate(number);
    }
}
