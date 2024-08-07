using CarAuction.Application.Common.Interfaces;
using FluentValidation;

namespace CarAuction.Application.Commands.CreateBid;

public class CreateBidCommandValidator : AbstractValidator<CreateBidCommand>
{
    private string _bidErrorMessage = string.Empty;

    public CreateBidCommandValidator(ICarAuctionContext db)
    {
        RuleFor(e => e.AuctionId)
            .NotEmpty();
        RuleFor(e => e.Value)
            .Must((command, _) => BeHigherThanLastBidOrIntitialBid(command, db, out _bidErrorMessage))
            .WithMessage(m => _bidErrorMessage)
            .NotEmpty()
            .GreaterThan(0);
    }

    #region private
    private static bool BeHigherThanLastBidOrIntitialBid(
        CreateBidCommand command, ICarAuctionContext db, out string bidErrorMessage)
    {
        var minimumBidValue = db.Auction
            .Where(e => e.Id == command.AuctionId)
            .Select(e => (decimal?)e.Bids.Max(e => e.Value) ?? e.Car.StartingBid)
            .FirstOrDefault();

        bidErrorMessage = $"Must place a bid higher than '{minimumBidValue}'.";

        return minimumBidValue is 0 || minimumBidValue < command.Value;
    }
    #endregion
}
