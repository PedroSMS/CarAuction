using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;

namespace CarAuction.Application.Commands.CreateCar;

public interface ICreateCarCommandAdapter
{
    Vehicle GetCarFrom(CreateCarCommand command);
}

public class CreateCarCommandAdapter : ICreateCarCommandAdapter
{
    public Vehicle GetCarFrom(CreateCarCommand command)
    {
        return command.TypeId switch
        {
            ECarType.Hatchback => new Hatchback
            {
                Identifier = command.Identifier,
                Manufacturer = command.Manufacturer,
                Model = command.Model,
                NumberOfDoors = command.NumberOfDoors.Value,
                StartingBid = command.StartingBid,
                Year = command.Year,
            },
            ECarType.Sedan => new Sedan
            {
                Identifier = command.Identifier,
                Manufacturer = command.Manufacturer,
                Model = command.Model,
                NumberOfDoors = command.NumberOfDoors.Value,
                StartingBid = command.StartingBid,
                Year = command.Year,
            },
            ECarType.Suv => new Suv
            {
                Identifier = command.Identifier,
                Manufacturer = command.Manufacturer,
                Model = command.Model,
                NumberOfSeats = command.NumberOfSeats.Value,
                StartingBid = command.StartingBid,
                Year = command.Year,
            },
            ECarType.Truck => new Truck
            {
                Identifier = command.Identifier,
                Manufacturer = command.Manufacturer,
                Model = command.Model,
                LoadCapacity = command.LoadCapacity.Value,
                StartingBid = command.StartingBid,
                Year = command.Year,
            },
            _ => throw new ArgumentOutOfRangeException(nameof(CreateCarCommandAdapter))
        };
    }
}
