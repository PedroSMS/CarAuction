using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using FluentValidation;
using MediatR;

namespace CarAuction.Application.Commands.CreateCar;

public class CreateCarCommandHandler(
    ICarAuctionContext db,
    IValidator<CreateCarCommand> validator,
    ICreateCarCommandAdapter adapter) : IRequestHandler<CreateCarCommand, Result<Vehicle>>
{
    private readonly ICarAuctionContext _db = db;
    private readonly IValidator<CreateCarCommand> _validator = validator;
    private readonly ICreateCarCommandAdapter _adapter = adapter;

    public async Task<Result<Vehicle>> Handle(CreateCarCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid is false)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        var car = _adapter.GetCarFrom(request);

        _db.Vehicle.Add(car);
        await _db.SaveChangesAsync(cancellationToken);

        return Result.Success(car);
    }
}
