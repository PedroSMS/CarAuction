namespace CarAuction.Application.Commands.CreateBid;

public class CreateBidCommandRequest
{
    public Guid AuctionId { get; set; }
    public decimal Value { get; set; }

    public CreateBidCommand ToCommand()
    {
        return new(Value, AuctionId);
    }
}
