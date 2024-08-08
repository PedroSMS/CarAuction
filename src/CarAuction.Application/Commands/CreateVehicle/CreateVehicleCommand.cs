using Ardalis.Result;
using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;
using MediatR;

namespace CarAuction.Application.Commands.CreateVehicle;

public class CreateVehicleCommand(CreateVehicleCommandRequest request) : IRequest<Result<Vehicle>>
{
    public EVehicleType TypeId { get; private set; } = (EVehicleType)request.TypeId;
    public string Identifier { get; private set; } = request.Identifier;
    public string Manufacturer { get; private set; } = request.Manufacturer;
    public string Model { get; private set; } = request.Model;
    public int Year { get; private set; } = request.Year;
    public decimal OpeningBid { get; private set; } = request.OpeningBid;
    public int? NumberOfDoors { get; private set; } = request.NumberOfDoors;
    public int? NumberOfSeats { get; private set; } = request.NumberOfSeats;
    public int? LoadCapacity { get; private set; } = request.LoadCapacity;
}
