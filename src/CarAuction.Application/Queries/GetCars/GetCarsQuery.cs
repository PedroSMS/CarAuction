using MediatR;

namespace CarAuction.Application.Queries.GetCars;

public class GetCarsQuery : IRequest<List<GetCarsQueryResponse>>;
