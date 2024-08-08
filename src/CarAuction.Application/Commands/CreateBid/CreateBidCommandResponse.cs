namespace CarAuction.Application.Commands.CreateBid;

public record CreateBidCommandResponse
{
    public Guid Id { get; init; }
    public decimal Value { get; init; }
    public DateTime PlacedAtUtc { get; init; }
    public Guid AuctionId { get; init; }
}
