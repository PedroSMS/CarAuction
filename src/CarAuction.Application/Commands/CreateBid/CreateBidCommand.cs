using Ardalis.Result;
using CarAuction.Domain.Entities;
using MediatR;

namespace CarAuction.Application.Commands.CreateBid;

public record CreateBidCommand(decimal Value, Guid AuctionId) : IRequest<Result<Bid>>;
