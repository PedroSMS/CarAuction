using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using CarAuction.Domain.Enum;
using MediatR;

namespace CarAuction.Application.Commands.AddCar;

public class CreateCarCommandHandler(IDatabase database) : IRequestHandler<CreateCarCommand, Guid>
{
    private readonly IDatabase _database = database;

    public Task<Guid> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var car = GetCar(request);

        if (car is null)
        {
            // TODO
        }

        _database.Cars.Add(car);

        return Task.FromResult(car?.Id ?? Guid.Empty);
    }

    private Vehicle? GetCar(CreateCarCommand request)
    {
        return request.TypeId switch
        {
            ECarType.Hatchback => new HatchBack
            {
                Identifier = request.Identifier,
                Manufacturer = request.Manufacturer,
                NumberOfDoors = request.NumberOfDoors.Value,
                StartingBid = request.StartingBid,
                Year = request.Year,
                OwnerId = Guid.NewGuid() // TODO
            },
            ECarType.Sedan => new Sedan
            {
                Identifier = request.Identifier,
                Manufacturer = request.Manufacturer,
                NumberOfDoors = request.NumberOfDoors.Value,
                StartingBid = request.StartingBid,
                Year = request.Year,
                OwnerId = Guid.NewGuid() // TODO
            },
            ECarType.Suv => new Suv
            {
                Identifier = request.Identifier,
                Manufacturer = request.Manufacturer,
                NumberOfSeats = request.NumberOfSeats.Value,
                StartingBid = request.StartingBid,
                Year = request.Year,
                OwnerId = Guid.NewGuid() // TODO
            },
            ECarType.Truck => new Truck
            {
                Identifier = request.Identifier,
                Manufacturer = request.Manufacturer,
                LoadCapacity = request.LoadCapacity.Value,
                StartingBid = request.StartingBid,
                Year = request.Year,
                OwnerId = Guid.NewGuid() // TODO
            },
            _ => throw new Exception(nameof(CreateCarCommand))
        };
    }
}
