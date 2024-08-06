using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;

namespace CarAuction.Application.Commands.CreateCar;

public interface ICreateCarCommandAdapter
{
    Vehicle GetCarFrom(CreateCarCommand request);
}

public class CreateCarCommandAdapter : ICreateCarCommandAdapter
{
    public Vehicle GetCarFrom(CreateCarCommand request)
    {
        return request.TypeId switch
        {
            ECarType.Hatchback => new Hatchback
            {
                Identifier = request.Identifier,
                Manufacturer = request.Manufacturer,
                Model = request.Model,
                NumberOfDoors = request.NumberOfDoors!.Value,
                StartingBid = request.StartingBid,
                Year = request.Year,
            },
            ECarType.Sedan => new Sedan
            {
                Identifier = request.Identifier,
                Manufacturer = request.Manufacturer,
                Model = request.Model,
                NumberOfDoors = request.NumberOfDoors!.Value,
                StartingBid = request.StartingBid,
                Year = request.Year,
            },
            ECarType.Suv => new Suv
            {
                Identifier = request.Identifier,
                Manufacturer = request.Manufacturer,
                Model = request.Model,
                NumberOfSeats = request.NumberOfSeats!.Value,
                StartingBid = request.StartingBid,
                Year = request.Year,
            },
            ECarType.Truck => new Truck
            {
                Identifier = request.Identifier,
                Manufacturer = request.Manufacturer,
                Model = request.Model,
                LoadCapacity = request.LoadCapacity!.Value,
                StartingBid = request.StartingBid,
                Year = request.Year,
            },
            _ => throw new ArgumentOutOfRangeException(nameof(CreateCarCommandAdapter))
        };
    }
}
