namespace CarAuction.Domain.Entities
{
    public class Auction
    {
        public Guid Id { get; set; }
        public DateTime? FinishedAtUtc { get; set; }

        public Guid CarId { get; set; }
        public Vehicle Car { get; set; } = null!;

        public ICollection<Bid> Bids { get; set; } = [];
    }
}