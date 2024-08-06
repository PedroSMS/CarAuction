namespace CarAuction.Application.Commands.CreateAuction;

public class CreateAuctionCommandRequest
{
    public Guid CarId { get; set; }

    public CreateAuctionCommand ToCommand()
    {
        return new(CarId);
    }
}
