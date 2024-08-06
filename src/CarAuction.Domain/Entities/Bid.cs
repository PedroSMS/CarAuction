namespace CarAuction.Domain.Entities;

public class Bid
{
    public Guid Id { get; set; }
    public required decimal Value { get; set; }
    public DateTime PlacedAtUtc { get; set; }

    public Guid AuctionId { get; set; }
    public Auction Auction { get; set; } = null!;
}