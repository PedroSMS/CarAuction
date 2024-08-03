using MediatR;

namespace CarAuction.Application.Queries.GetCarById;

public record GetCarByIdQuery(Guid Id) : IRequest<GetCarByIdQueryResponse?>;