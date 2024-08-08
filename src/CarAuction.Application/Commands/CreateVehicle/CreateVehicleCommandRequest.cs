namespace CarAuction.Application.Commands.CreateVehicle;

public class CreateVehicleCommandRequest
{
    public int TypeId { get; set; }
    public string Identifier { get; set; } = null!;
    public string Manufacturer { get; set; } = null!;
    public string Model { get; set; } = null!;
    public int Year { get; set; }
    public decimal OpeningBid { get; set; }
    public int? NumberOfDoors { get; set; }
    public int? NumberOfSeats { get; set; }
    public int? LoadCapacity { get; set; }

    public CreateVehicleCommand ToCommand() => new(this);
}
