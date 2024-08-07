using Ardalis.Result;
using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;
using MediatR;

namespace CarAuction.Application.Commands.CreateCar;

public class CreateCarCommand(CreateCarCommandRequest request) : IRequest<Result<Vehicle>>
{
    public ECarType TypeId { get; private set; } = (ECarType)request.TypeId;
    public string Identifier { get; private set; } = request.Identifier;
    public string Manufacturer { get; private set; } = request.Manufacturer;
    public string Model { get; private set; } = request.Model;
    public int Year { get; private set; } = request.Year;
    public decimal StartingBid { get; private set; } = request.StartingBid;
    public int? NumberOfDoors { get; private set; } = request.NumberOfDoors;
    public int? NumberOfSeats { get; private set; } = request.NumberOfSeats;
    public int? LoadCapacity { get; private set; } = request.LoadCapacity;
}
