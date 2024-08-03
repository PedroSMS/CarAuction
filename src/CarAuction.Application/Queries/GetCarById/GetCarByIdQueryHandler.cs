using CarAuction.Application.Common.Interfaces;
using MediatR;

namespace CarAuction.Application.Queries.GetCarById;

public class GetCarByIdQueryHandler(IDatabase database) : IRequestHandler<GetCarByIdQuery, GetCarByIdQueryResponse?>
{
    private readonly IDatabase _database = database;

    public Task<GetCarByIdQueryResponse?> Handle(GetCarByIdQuery request, CancellationToken cancellationToken)
    {
        var car = _database.Cars
            .Select(GetCarByIdQueryResponse.Projection)
            .SingleOrDefault(e => e.Id == request.Id);

        if(car == null) 
        {
            // TODO
        }

        return Task.FromResult(car);
    }
}
