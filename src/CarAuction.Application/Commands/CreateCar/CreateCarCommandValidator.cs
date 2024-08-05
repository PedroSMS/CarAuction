using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Application.Commands.CreateCar;

public class CreateCarCommandValidator : AbstractValidator<CreateCarCommand>
{
    public CreateCarCommandValidator(ICarAuctionContext db)
    {
        RuleFor(r => r.TypeId)
            .NotEmpty()
            .IsInEnum();
        RuleFor(r => r.Year)
            .InclusiveBetween(1885, DateTime.UtcNow.Year);
        RuleFor(r => r.Manufacturer)
            .NotEmpty();
        RuleFor(r => r.Model)
            .NotEmpty();
        RuleFor(r => r.Identifier)
            .NotEmpty()
            .MustAsync(async (i, ct) => await CheckIfIdentifierDoesNotExistsInDatabase(db, i, ct))
            .WithMessage("Identifier already exists in the database.");
        RuleFor(r => r.StartingBid)
            .NotEmpty();
        RuleFor(r => r.NumberOfDoors)
            .NotEmpty()
            .GreaterThan(0)
            .When(r => r.TypeId == ECarType.Hatchback || r.TypeId == ECarType.Sedan);
        RuleFor(r => r.NumberOfSeats)
            .NotEmpty()
            .GreaterThan(0)
            .When(r => r.TypeId == ECarType.Suv);
        RuleFor(r => r.LoadCapacity)
            .NotEmpty()
            .GreaterThan(0)
            .When(r => r.TypeId == ECarType.Truck);
    }

    private static async Task<bool> CheckIfIdentifierDoesNotExistsInDatabase(
        ICarAuctionContext db, string identifier, CancellationToken cancellationToken)
    {
        return await db.Vehicle.AnyAsync(e => e.Identifier == identifier, cancellationToken) is false;
    }
}
