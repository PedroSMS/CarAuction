using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using FluentValidation;
using MediatR;

namespace CarAuction.Application.Commands.CreateBid;

public class CreateBidCommandHandler(
    ICarAuctionContext db,
    IValidator<CreateBidCommand> validator,
    ICreateBidCommandAdapter adapter) : IRequestHandler<CreateBidCommand, Bid>
{
    private readonly ICarAuctionContext _db = db;
    private readonly IValidator<CreateBidCommand> _validator = validator;
    private readonly ICreateBidCommandAdapter _adapter = adapter;

    public async Task<Bid> Handle(CreateBidCommand request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid is false)
        {
            // TODO
            throw new ValidationException(validationResult.Errors);
        }

        var bid = _adapter.GetBidFrom(request);

        await _db.Bid.AddAsync(bid, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return bid;
    }
}
