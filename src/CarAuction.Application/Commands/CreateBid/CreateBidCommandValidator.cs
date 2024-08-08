using CarAuction.Application.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Application.Commands.CreateBid;

public class CreateBidCommandValidator : AbstractValidator<CreateBidCommand>
{
    private string _bidErrorMessage = string.Empty;

    public CreateBidCommandValidator(ICarAuctionContext db)
    {
        RuleFor(e => e.AuctionId)
            .NotEmpty();
        RuleFor(e => e.Value)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .Must((command, _) => BeHigherThanLastBidOrIntitialBid(command, db, out _bidErrorMessage))
            .WithMessage(m => _bidErrorMessage);
    }

    #region private
    private static bool BeHigherThanLastBidOrIntitialBid(
        CreateBidCommand command, ICarAuctionContext db, out string bidErrorMessage)
    {
        // Changes were made to accomodate EF in memory database
        //var minimumBidValue = db.Auction
        //    .Where(e => e.Id == command.AuctionId)
        //    .Select(e => (decimal?)e.Bids.Max(e => e.Value) ?? e.Vehicle.OpeningBid)
        //    .FirstOrDefault();
        //bidErrorMessage = $"Must place a bid higher than '{minimumBidValue}'.";

        //return minimumBidValue is 0 || minimumBidValue < command.Value;

        var auction = db.Auction
            .AsNoTracking()
            .Include(e => e.Bids)
            .Include(e => e.Vehicle)
            .FirstOrDefault(e => e.Id == command.AuctionId);

        var minimumBidValue = auction?.Bids.Any() is true
            ? auction!.Bids.Max(e => e.Value)
            : auction?.Vehicle.OpeningBid;

        bidErrorMessage = $"Must place a bid higher than '{minimumBidValue}'.";

        return minimumBidValue is 0 or null || minimumBidValue < command.Value;
    }
    #endregion
}
