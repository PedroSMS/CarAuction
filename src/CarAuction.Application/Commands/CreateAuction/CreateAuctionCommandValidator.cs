using CarAuction.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Application.Commands.CreateAuction;

public class CreateAuctionCommandValidator : AbstractValidator<CreateAuctionCommand>
{
    public CreateAuctionCommandValidator(ICarAuctionContext db)
    {
        RuleFor(e => e.VehicleId)
            .NotEmpty()
            .MustAsync(async (id, ct) => await DoNotExistInActiveAuctionAsync(db, id, ct))
            .WithMessage("Car is already in an active auction.");
    }

    #region private

    private static async Task<bool> DoNotExistInActiveAuctionAsync(
        ICarAuctionContext db, Guid id, CancellationToken cancellationToken)
    {
        return await db.Auction
            .AnyAsync(e => e.VehicleId == id && !e.FinishedAtUtc.HasValue, 
                cancellationToken) is false;
    }
    #endregion
}
