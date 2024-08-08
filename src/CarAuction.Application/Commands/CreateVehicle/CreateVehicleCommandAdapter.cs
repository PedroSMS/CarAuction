using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;

namespace CarAuction.Application.Commands.CreateVehicle;

public interface ICreateVehicleCommandAdapter
{
    Vehicle GetVehicleFrom(CreateVehicleCommand request);
}

public class CreateVehicleCommandAdapter : ICreateVehicleCommandAdapter
{
    public Vehicle GetVehicleFrom(CreateVehicleCommand request)
    {
        return request.TypeId switch
        {
            EVehicleType.Hatchback => new Hatchback
            {
                Identifier = request.Identifier,
                Manufacturer = request.Manufacturer,
                Model = request.Model,
                NumberOfDoors = request.NumberOfDoors!.Value,
                OpeningBid = request.OpeningBid,
                Year = request.Year,
            },
            EVehicleType.Sedan => new Sedan
            {
                Identifier = request.Identifier,
                Manufacturer = request.Manufacturer,
                Model = request.Model,
                NumberOfDoors = request.NumberOfDoors!.Value,
                OpeningBid = request.OpeningBid,
                Year = request.Year,
            },
            EVehicleType.Suv => new Suv
            {
                Identifier = request.Identifier,
                Manufacturer = request.Manufacturer,
                Model = request.Model,
                NumberOfSeats = request.NumberOfSeats!.Value,
                OpeningBid = request.OpeningBid,
                Year = request.Year,
            },
            EVehicleType.Truck => new Truck
            {
                Identifier = request.Identifier,
                Manufacturer = request.Manufacturer,
                Model = request.Model,
                LoadCapacity = request.LoadCapacity!.Value,
                OpeningBid = request.OpeningBid,
                Year = request.Year,
            },
            _ => throw new ArgumentOutOfRangeException(nameof(GetVehicleFrom))
        };
    }
}
