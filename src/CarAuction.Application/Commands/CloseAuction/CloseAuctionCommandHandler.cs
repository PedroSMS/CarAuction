using CarAuction.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Application.Commands.CloseAuction;

public class CloseAuctionCommandHandler(ICarAuctionContext db) : IRequestHandler<CloseAuctionCommand, Unit>
{
    private readonly ICarAuctionContext _db = db;

    public async Task<Unit> Handle(CloseAuctionCommand request, CancellationToken cancellationToken)
    {
        var auction = await _db.Auction
            .FirstOrDefaultAsync(e => e.Id == request.Id, 
                cancellationToken);

        // TODO
        ArgumentNullException.ThrowIfNull(auction, nameof(auction));

        auction.FinishedAtUtc = DateTime.UtcNow;
        await _db.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
