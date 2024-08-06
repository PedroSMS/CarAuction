using CarAuction.Domain.Entities;

namespace CarAuction.Application.Commands.CreateBid;

public interface ICreateBidCommandAdapter
{
    Bid GetBidFrom(CreateBidCommand request);
}

public class CreateBidCommandAdapter : ICreateBidCommandAdapter
{
    public Bid GetBidFrom(CreateBidCommand request)
    {
        return new Bid
        {
            AuctionId = request.AuctionId,
            Value = request.Value,
            PlacedAtUtc = DateTime.UtcNow
        };
    }
}
