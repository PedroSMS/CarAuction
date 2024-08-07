using Ardalis.Result;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

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
        var vehicleDoesNotExists = await _db.Vehicle
            .AnyAsync(e => e.Id == request.CarId, 
                cancellationToken) is false;

        if (vehicleDoesNotExists) return Result.NotFound($"Unable to find vehicle with id '{request.CarId}'");

        var auction = _adapter.GetAuctionFrom(request);

        _db.Auction.Add(auction);
        await _db.SaveChangesAsync(cancellationToken);

        return auction;
    }
}
