namespace CarAuction.Domain.Entities
{
    public class Auction
    {
        public Guid Id { get; set; }
        public DateTime? FinishedAtUtc { get; set; }

        public Guid CarId { get; set; }
        public required Car Car { get; set; }

        public required ICollection<AuctionBid> Bids { get; set; } = [];
    }
}