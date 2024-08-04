using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;
using MediatR;

namespace CarAuction.Application.Commands.AddCar;

public class CreateCarCommandHandler(ICarAuctionContext db) : IRequestHandler<CreateCarCommand, Guid>
{
    private readonly ICarAuctionContext _db = db;

    public async Task<Guid> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var car = GetCar(request);

        if (car is null)
        {
            // TODO
        }

        _db.Vehicle.Add(car);
        await _db.SaveChangesAsync(cancellationToken);

        return car.Id;
    }

    private Vehicle GetCar(CreateCarCommand request)
    {
        return request.TypeId switch
        {
            ECarType.Hatchback => new Hatchback
            {
                Identifier = request.Identifier,
                Manufacturer = request.Manufacturer,
                NumberOfDoors = request.NumberOfDoors.Value,
                StartingBid = request.StartingBid,
                Year = request.Year,
            },
            ECarType.Sedan => new Sedan
            {
                Identifier = request.Identifier,
                Manufacturer = request.Manufacturer,
                NumberOfDoors = request.NumberOfDoors.Value,
                StartingBid = request.StartingBid,
                Year = request.Year,
            },
            ECarType.Suv => new Suv
            {
                Identifier = request.Identifier,
                Manufacturer = request.Manufacturer,
                NumberOfSeats = request.NumberOfSeats.Value,
                StartingBid = request.StartingBid,
                Year = request.Year,
            },
            ECarType.Truck => new Truck
            {
                Identifier = request.Identifier,
                Manufacturer = request.Manufacturer,
                LoadCapacity = request.LoadCapacity.Value,
                StartingBid = request.StartingBid,
                Year = request.Year,
            },
            _ => throw new Exception(nameof(CreateCarCommand))
        };
    }
}
