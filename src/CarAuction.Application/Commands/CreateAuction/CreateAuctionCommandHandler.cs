using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using FluentValidation;
using MediatR;

namespace CarAuction.Application.Commands.CreateAuction;

public class CreateAuctionCommandHandler(
    IValidator<CreateAuctionCommand> validator,
    ICarAuctionContext db,
    ICreateAuctionCommandAdapter adapter) : IRequestHandler<CreateAuctionCommand, Result<Auction>>
{
    private readonly ICarAuctionContext _db = db;
    private readonly IValidator<CreateAuctionCommand> _validator = validator;
    private readonly ICreateAuctionCommandAdapter _adapter = adapter;

    public async Task<Result<Auction>> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid is false)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        var auction = _adapter.GetAuctionFrom(request);

        _db.Auction.Add(auction);
        await _db.SaveChangesAsync(cancellationToken);

        return Result.Success(auction);
    }
}
