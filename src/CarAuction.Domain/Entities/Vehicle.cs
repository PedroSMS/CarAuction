using CarAuction.Domain.Interfaces;

namespace CarAuction.Domain.Entities;

public abstract class Vehicle : IBiddable
{
    public Guid Id { get; } = Guid.NewGuid();
    public required string Identifier { get; set; }
    public required string Manufacturer { get; set; }
    public int Year { get; set; }
    public decimal StartingBid { get; set; }
}