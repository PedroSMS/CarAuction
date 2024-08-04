using CarAuction.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Application.Queries.GetCars;

public class GetCarsQueryHandler(ICarAuctionContext db) : IRequestHandler<GetCarsQuery, List<GetCarsQueryResponse>>
{
    private readonly ICarAuctionContext _db = db;

    public async Task<List<GetCarsQueryResponse>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
    {
        var cars = await _db.Vehicle
            .Select(GetCarsQueryResponse.Projection)
            .ToListAsync(cancellationToken);

        return cars;
    }
}
