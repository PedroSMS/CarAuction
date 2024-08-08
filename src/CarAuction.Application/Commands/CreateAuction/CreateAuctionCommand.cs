using Ardalis.Result;
using CarAuction.Domain.Entities;
using MediatR;

namespace CarAuction.Application.Commands.CreateAuction;

public record CreateAuctionCommand(Guid VehicleId) : IRequest<Result<Auction>>;
