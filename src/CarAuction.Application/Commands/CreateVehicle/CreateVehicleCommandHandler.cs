using Ardalis.Result;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using MediatR;

namespace CarAuction.Application.Commands.CreateVehicle;

public class CreateVehicleCommandHandler(
    ICarAuctionContext db,
    ICreateVehicleCommandAdapter adapter) 
    : IRequestHandler<CreateVehicleCommand, Result<Vehicle>>
{
    private readonly ICarAuctionContext _db = db;
    private readonly ICreateVehicleCommandAdapter _adapter = adapter;

    public async Task<Result<Vehicle>> Handle(CreateVehicleCommand request, CancellationToken cancellationToken)
    {
        var vehicle = _adapter.GetVehicleFrom(request);

        _db.Vehicle.Add(vehicle);
        await _db.SaveChangesAsync(cancellationToken);

        return vehicle;
    }
}
