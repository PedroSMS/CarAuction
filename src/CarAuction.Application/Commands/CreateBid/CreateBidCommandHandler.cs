using Ardalis.Result;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Application.Commands.CreateBid;

public class CreateBidCommandHandler(
    ICarAuctionContext db,
    IValidator<CreateBidCommand> validator,
    ICreateBidCommandAdapter adapter) : IRequestHandler<CreateBidCommand, Result<Bid>>
{
    private readonly ICarAuctionContext _db = db;
    private readonly IValidator<CreateBidCommand> _validator = validator;
    private readonly ICreateBidCommandAdapter _adapter = adapter;

    public async Task<Result<Bid>> Handle(CreateBidCommand request, CancellationToken cancellationToken)
    {
        var auctionDoesNotExistsOrItIsClosed = await _db.Auction
            .AnyAsync(e => e.Id == request.AuctionId && !e.FinishedAtUtc.HasValue, 
                cancellationToken) is false;

        if (auctionDoesNotExistsOrItIsClosed) 
            return Result.NotFound($"Unable to find auction with id '{request.AuctionId}' or auction is already closed-");

        var bid = _adapter.GetBidFrom(request);

        _db.Bid.Add(bid);
        await _db.SaveChangesAsync(cancellationToken);

        return bid;
    }
}
