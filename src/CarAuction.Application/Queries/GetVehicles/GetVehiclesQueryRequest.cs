namespace CarAuction.Application.Queries.GetVehicles;

public record GetVehiclesQueryRequest
{
    public string? Manufacturer { get; init; }
    public string? Model { get; init; }
    public int? Year { get; init; }

    public int? TypeId { get; init; }
}
