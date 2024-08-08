using MediatR;

namespace CarAuction.Application.Queries.GetVehicles;

public record GetVehiclesQuery(GetVehiclesQueryRequest Request) : IRequest<List<GetVehiclesQueryResponse>>;
