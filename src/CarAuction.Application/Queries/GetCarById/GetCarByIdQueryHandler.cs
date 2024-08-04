using CarAuction.Application.Common.Interfaces;
using MediatR;

namespace CarAuction.Application.Queries.GetCarById;

public class GetCarByIdQueryHandler(ICarAuctionContext db) : IRequestHandler<GetCarByIdQuery, GetCarByIdQueryResponse?>
{
    private readonly ICarAuctionContext _db = db;

    public Task<GetCarByIdQueryResponse?> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        var car = _db.Vehicle
            .Select(GetCarByIdQueryResponse.Projection)
            .SingleOrDefault(e => e.Id == request.Id);

        if (car == null) 
        {
            // TODO
        }

        return Task.FromResult(car);
    }
}
