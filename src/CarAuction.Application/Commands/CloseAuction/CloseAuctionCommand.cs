using MediatR;

namespace CarAuction.Application.Commands.CloseAuction;

public record CloseAuctionCommand(Guid Id) : IRequest<Unit>;