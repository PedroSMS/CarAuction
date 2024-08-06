using CarAuction.Application.Commands.CreateCar;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using FluentValidation;
using MediatR;

namespace CarAuction.Application.Commands.CreateAuction;

public class CreateAuctionCommandHandler(
    IValidator<CreateAuctionCommand> validator,
    ICarAuctionContext db,
    ICreateAuctionCommandAdapter adapter) : IRequestHandler<CreateAuctionCommand, Auction>
{
    private readonly ICarAuctionContext _db = db;
    private readonly IValidator<CreateAuctionCommand> _validator = validator;
    private readonly ICreateAuctionCommandAdapter _adapter = adapter;

    public async Task<Auction> Handle(CreateAuctionCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid is false)
        {
            // TODO
            throw new ValidationException(validationResult.Errors);
        }

        var auction = _adapter.GetAuctionFrom(request);

        _db.Auction.Add(auction);
        await _db.SaveChangesAsync(cancellationToken);

        return auction;
    }
}
