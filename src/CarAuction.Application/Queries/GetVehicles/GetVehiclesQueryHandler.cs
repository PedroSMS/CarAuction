using CarAuction.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Application.Queries.GetVehicles;

public class GetVehiclesQueryHandler(ICarAuctionContext db) : IRequestHandler<GetVehiclesQuery, List<GetVehiclesQueryResponse>>
{
    private readonly ICarAuctionContext _db = db;

    public async Task<List<GetVehiclesQueryResponse>> Handle(GetVehiclesQuery request, CancellationToken cancellationToken)
    {
        return await _db.Vehicle
            .AddRequestFilters(request.Request)
            .Select(GetVehiclesQueryResponse.Projection)
            .ToListAsync(cancellationToken);
    }
}
