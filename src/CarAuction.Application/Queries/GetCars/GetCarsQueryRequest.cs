﻿namespace CarAuction.Application.Queries.GetCars;

public record GetCarsQueryRequest
{
    public string? Manufacturer { get; init; }
    public string? Model { get; init; }
    public int? Year { get; init; }

    public int? TypeId { get; init; }
}
