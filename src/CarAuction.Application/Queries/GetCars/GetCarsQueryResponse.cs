using CarAuction.Application.Queries.GetCarById;
using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;
using System.Linq.Expressions;

namespace CarAuction.Application.Queries.GetCars;

public class GetCarsQueryResponse
{
    public Guid Id { get; init; }
    public int? TypeId { get; init; }
    public string? Type { get; init; }
    public string Identifier { get; init; }
    public string Manufacturer { get; init; }
    public int Year { get; init; }
    public decimal StartingBid { get; init; }
    public int? NumberOfDoors { get; init; }
    public int? NumberOfSeats { get; init; }
    public int? LoadCapacity { get; init; }

    public static Expression<Func<Vehicle, GetCarsQueryResponse>> Projection =>
        vehicle => new GetCarsQueryResponse
        {
            Id = vehicle.Id,
            Identifier = vehicle.Identifier,
            Manufacturer = vehicle.Manufacturer,
            Year = vehicle.Year,
            StartingBid = vehicle.StartingBid,
            Type = vehicle is Hatchback ? nameof(ECarType.Hatchback)
                : vehicle is Sedan ? nameof(ECarType.Sedan)
                : vehicle is Suv ? nameof(ECarType.Suv)
                : vehicle is Truck ? nameof(ECarType.Truck)
                : null,
            TypeId = vehicle is Hatchback ? (int)ECarType.Hatchback
                : vehicle is Sedan ? (int)ECarType.Sedan
                : vehicle is Suv ? (int)ECarType.Suv
                : vehicle is Truck ? (int)ECarType.Truck
                : null,
            NumberOfDoors = (int?)((Hatchback)vehicle).NumberOfDoors ?? ((Sedan)vehicle).NumberOfDoors,
            NumberOfSeats = ((Suv)vehicle).NumberOfSeats,
            LoadCapacity = ((Truck)vehicle).LoadCapacity
        };
}
