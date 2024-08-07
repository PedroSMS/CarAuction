using Ardalis.Result;
using CarAuction.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Application.Commands.CloseAuction;

public class CloseAuctionCommandHandler(ICarAuctionContext db) : IRequestHandler<CloseAuctionCommand, Result<Unit>>
{
    private readonly ICarAuctionContext _db = db;

    public async Task<Result<Unit>> Handle(CloseAuctionCommand request, CancellationToken cancellationToken)
    {
        var auction = await _db.Auction
            .FirstOrDefaultAsync(e => e.Id == request.Id && !e.FinishedAtUtc.HasValue, 
                cancellationToken);

        if (auction is null) return Result.NotFound($"Unable to find auction with id '{request.Id}'");

        auction!.FinishedAtUtc = DateTime.UtcNow;
        await _db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
