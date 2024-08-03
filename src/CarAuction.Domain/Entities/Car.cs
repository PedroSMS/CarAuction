namespace CarAuction.Domain.Entities;

public abstract class Car
{
    public Guid Id { get; set; }
    public required string Identifier { get; set; }
    public required string Manufacturer { get; set; }
    public required int Year { get; set; }
    public required decimal StartingBid { get; set; }
    
    public Guid OwnerId { get; set; }
    public required User Owner { get; set; }
}