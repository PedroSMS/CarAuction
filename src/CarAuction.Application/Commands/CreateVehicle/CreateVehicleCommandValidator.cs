using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Enums;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Application.Commands.CreateVehicle;

public class CreateVehicleCommandValidator : AbstractValidator<CreateVehicleCommand>
{
    public CreateVehicleCommandValidator(ICarAuctionContext db)
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
            .MustAsync(async (i, ct) => await DoesNotExistsInDatabaseAsync(db, i, ct))
            .WithMessage("Identifier already exists in the database.");
        RuleFor(r => r.OpeningBid)
            .NotEmpty();
        RuleFor(r => r.NumberOfDoors)
            .NotEmpty()
            .GreaterThan(0)
            .When(r => r.TypeId == EVehicleType.Hatchback || r.TypeId == EVehicleType.Sedan);
        RuleFor(r => r.NumberOfSeats)
            .NotEmpty()
            .GreaterThan(0)
            .When(r => r.TypeId == EVehicleType.Suv);
        RuleFor(r => r.LoadCapacity)
            .NotEmpty()
            .GreaterThan(0)
            .When(r => r.TypeId == EVehicleType.Truck);
    }

    #region private
    private static async Task<bool> DoesNotExistsInDatabaseAsync(
        ICarAuctionContext db, string identifier, CancellationToken cancellationToken)
    {
        return await db.Vehicle.AnyAsync(e => e.Identifier == identifier, cancellationToken) is false;
    }
    #endregion
}
