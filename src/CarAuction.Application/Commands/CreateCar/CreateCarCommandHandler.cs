using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;
using FluentValidation;
using MediatR;

namespace CarAuction.Application.Commands.CreateCar;

public class CreateCarCommandHandler(
    ICarAuctionContext db, 
    IValidator<CreateCarCommand> validator) : IRequestHandler<CreateCarCommand, Guid>
{
    private readonly ICarAuctionContext _db = db;
    private readonly IValidator<CreateCarCommand> _validator = validator;

    public async Task<Guid> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid is false)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var car = GetCar(request);

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
