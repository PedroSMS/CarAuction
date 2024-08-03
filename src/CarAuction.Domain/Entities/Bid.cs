namespace CarAuction.Domain.Entities;

public class Bid
{
    public Guid Id { get; set; }
    public required decimal Value { get; set; }

    public Guid AuctionId { get; set; }
    public required Auction Auction { get; set; }
    public Guid UserId { get; set; }
    public required User User { get; set; }
}