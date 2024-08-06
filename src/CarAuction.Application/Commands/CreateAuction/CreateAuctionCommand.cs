using CarAuction.Domain.Entities;
using MediatR;

namespace CarAuction.Application.Commands.CreateAuction;

public record CreateAuctionCommand(Guid CarId) : IRequest<Auction>;
