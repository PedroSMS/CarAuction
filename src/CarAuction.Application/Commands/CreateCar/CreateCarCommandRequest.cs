namespace CarAuction.Application.Commands.CreateCar;

public class CreateCarCommandRequest
{
    public int TypeId { get; set; }
    public string Identifier { get; set; }
    public string Manufacturer { get; set; }
    public int Year { get; set; }
    public decimal StartingBid { get; set; }
    public int? NumberOfDoors { get; set; }
    public int? NumberOfSeats { get; set; }
    public int? LoadCapacity { get; set; }

    public CreateCarCommand ToCommand() => new(this);
}
