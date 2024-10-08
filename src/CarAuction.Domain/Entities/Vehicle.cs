﻿using CarAuction.Domain.Interfaces;

namespace CarAuction.Domain.Entities;

public abstract class Vehicle : IBiddable
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string Identifier { get; set; }
    public required string Manufacturer { get; set; }
    public required string Model { get; set; }
    public int Year { get; set; }
    public decimal OpeningBid { get; set; }
}