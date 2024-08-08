namespace CarAuction.Application.Commands.CreateAuction;

public class CreateAuctionCommandRequest
{
    public Guid VehicleId { get; set; }

    public CreateAuctionCommand ToCommand()
    {
        return new(VehicleId);
    }
}
