using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Application.Queries.GetCars;

public class GetCarsQueryHandler(ICarAuctionContext db) : IRequestHandler<GetCarsQuery, List<GetCarsQueryResponse>>
{
    private readonly ICarAuctionContext _db = db;

    public async Task<List<GetCarsQueryResponse>> Handle(GetCarsQuery request, CancellationToken cancellationToken)
    {
        return await _db.Vehicle
            .AddRequestFilters(request.Request)
            .Select(GetCarsQueryResponse.Projection)
            .ToListAsync(cancellationToken);
    }
}

internal static class GetCarsQueryHandlerExtensions
{
    public static IQueryable<Vehicle> AddRequestFilters(this IQueryable<Vehicle> query, GetCarsQueryRequest request)
    {
        query = (ECarType?)request.TypeId switch
        {
            ECarType.Hatchback => query.Where(e => e.GetType() == typeof(Hatchback)),
            ECarType.Sedan => query.Where(e => e.GetType() == typeof(Sedan)),
            ECarType.Suv => query.Where(e => e.GetType() == typeof(Suv)),
            ECarType.Truck => query.Where(e => e.GetType() == typeof(Truck)),
            _ => query
        };

        if (string.IsNullOrWhiteSpace(request.Manufacturer) is false)
        {
            query = query.Where(m => m.Manufacturer ==  request.Manufacturer);
        }

        if (string.IsNullOrWhiteSpace(request.Model) is false)
        {
            query = query.Where(m => m.Model == request.Model);
        }

        if (request.Year.HasValue && request.Year > 0)
        {
            query = query.Where(m => m.Year == request.Year);
        }

        return query;
    }
}
