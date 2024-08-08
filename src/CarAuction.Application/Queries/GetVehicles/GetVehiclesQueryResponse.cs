using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;
using System.Linq.Expressions;

namespace CarAuction.Application.Queries.GetVehicles;

public class GetVehiclesQueryResponse
{
    public Guid Id { get; init; }
    public int? TypeId { get; init; }
    public string? Type { get; init; }
    public string Identifier { get; init; } = null!;
    public string Manufacturer { get; init; } = null!;
    public string Model { get; init; } = null!;
    public int Year { get; init; }
    public decimal OpeningBid { get; init; }
    public int? NumberOfDoors { get; init; }
    public int? NumberOfSeats { get; init; }
    public int? LoadCapacity { get; init; }

    public static Expression<Func<Vehicle, GetVehiclesQueryResponse>> Projection =>
        vehicle => new GetVehiclesQueryResponse
        {
            Id = vehicle.Id,
            Identifier = vehicle.Identifier,
            Manufacturer = vehicle.Manufacturer,
            Model = vehicle.Model,
            Year = vehicle.Year,
            OpeningBid = vehicle.OpeningBid,
            Type = vehicle is Hatchback ? nameof(EVehicleType.Hatchback)
                : vehicle is Sedan ? nameof(EVehicleType.Sedan)
                : vehicle is Suv ? nameof(EVehicleType.Suv)
                : vehicle is Truck ? nameof(EVehicleType.Truck)
                : null,
            TypeId = vehicle is Hatchback ? (int)EVehicleType.Hatchback
                : vehicle is Sedan ? (int)EVehicleType.Sedan
                : vehicle is Suv ? (int)EVehicleType.Suv
                : vehicle is Truck ? (int)EVehicleType.Truck
                : null,
            NumberOfDoors = (int?)((Hatchback)vehicle).NumberOfDoors ?? ((Sedan)vehicle).NumberOfDoors,
            NumberOfSeats = ((Suv)vehicle).NumberOfSeats,
            LoadCapacity = ((Truck)vehicle).LoadCapacity
        };
}

public static class GetVehiclesQueryResponseExtensions
{
    public static IQueryable<Vehicle> AddRequestFilters(this IQueryable<Vehicle> query, GetVehiclesQueryRequest request)
    {
        if (request.TypeId.HasValue)
        {
            query = (EVehicleType?)request.TypeId switch
            {
                EVehicleType.Hatchback => query.Where(e => e.GetType() == typeof(Hatchback)),
                EVehicleType.Sedan => query.Where(e => e.GetType() == typeof(Sedan)),
                EVehicleType.Suv => query.Where(e => e.GetType() == typeof(Suv)),
                EVehicleType.Truck => query.Where(e => e.GetType() == typeof(Truck)),
                _ => query.Where(e => e.GetType() == typeof(Vehicle)),
            };
        }

        if (string.IsNullOrWhiteSpace(request.Manufacturer) is false)
        {
            query = query.Where(m => m.Manufacturer == request.Manufacturer);
        }

        if (string.IsNullOrWhiteSpace(request.Model) is false)
        {
            query = query.Where(m => m.Model == request.Model);
        }

        if (request.Year.HasValue)
        {
            query = query.Where(m => m.Year == request.Year);
        }

        return query;
    }
}