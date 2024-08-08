namespace CarAuction.Domain.Entities
{
    public class Auction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime? FinishedAtUtc { get; set; }

        public Guid VehicleId { get; set; }
        public Vehicle Vehicle { get; set; } = null!;

        public ICollection<Bid> Bids { get; set; } = [];
    }
}