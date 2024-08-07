using Ardalis.Result;
using Ardalis.Result.FluentValidation;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using FluentValidation;
using MediatR;

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
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);

        if (validationResult.IsValid is false)
        {
            return Result.Invalid(validationResult.AsErrors());
        }

        var bid = _adapter.GetBidFrom(request);

        await _db.Bid.AddAsync(bid, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return Result.Success(bid);
    }
}
