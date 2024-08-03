namespace CarAuction.Domain.Entities;

public class AuctionBid
{
    public Guid Id { get; set; }
    public required decimal Bid { get; set; }

    public Guid AuctionId { get; set; }
    public required Auction Auction { get; set; }
    public Guid UserId { get; set; }
    public required User User { get; set; }
}