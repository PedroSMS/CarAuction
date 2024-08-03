using CarAuction.Domain.Entities;

namespace CarAuction.Application.Queries.GetCarById;

public class GetCarByIdQueryResponse
{
    public Guid Id { get; set; }
    public int TypeId { get; init; }
    public string Type { get; init; }
    public string Identifier { get; init; }
    public string Manufacturer { get; init; }
    public int Year { get; init; }
    public decimal StartingBid { get; init; }
    public int? NumberOfDoors { get; init; }
    public int? NumberOfSeats { get; init; }
    public int? LoadCapacity { get; init; }

    public static Func<Vehicle, GetCarByIdQueryResponse> Projection => vehicle =>
        new GetCarByIdQueryResponse
        {
            Id = vehicle.Id,
            Identifier = vehicle.Identifier,
            Manufacturer = vehicle.Manufacturer,
            Year = vehicle.Year,
            StartingBid = vehicle.StartingBid
        };
}
