using MediatR;

namespace CarAuction.Application.Queries.GetCars;

public record GetCarsQuery(GetCarsQueryRequest Request) : IRequest<List<GetCarsQueryResponse>>;
