using CarAuction.Domain.Enum;
using MediatR;

namespace CarAuction.Application.Commands.AddCar;

public class CreateCarCommand(CreateCarCommandRequest request) : IRequest<Guid>
{
    public ECarType TypeId { get; private set; } = (ECarType)request.TypeId;
    public string Identifier { get; private set; } = request.Identifier;
    public string Manufacturer { get; private set; } = request.Manufacturer;
    public int Year { get; private set; } = request.Year;
    public decimal StartingBid { get; private set; } = request.StartingBid;
    public int? NumberOfDoors { get; private set; } = request.NumberOfDoors;
    public int? NumberOfSeats { get; private set; } = request.NumberOfSeats;
    public int? LoadCapacity { get; private set; } = request.LoadCapacity;
}
