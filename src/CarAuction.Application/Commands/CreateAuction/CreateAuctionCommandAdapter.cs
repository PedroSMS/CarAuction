using CarAuction.Domain.Entities;

namespace CarAuction.Application.Commands.CreateAuction;

public interface ICreateAuctionCommandAdapter
{
    Auction GetAuctionFrom(CreateAuctionCommand request);
}

public class CreateAuctionCommandAdapter : ICreateAuctionCommandAdapter
{
    public Auction GetAuctionFrom(CreateAuctionCommand request)
    {
        return new Auction
        {
            CarId = request.CarId,
        };
    }
}
