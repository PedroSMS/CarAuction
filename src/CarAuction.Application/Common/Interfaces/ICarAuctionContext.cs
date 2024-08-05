using CarAuction.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.Application.Common.Interfaces;

public interface ICarAuctionContext
{
    DbSet<Vehicle> Vehicle { get; set; }
    DbSet<Auction> Auction { get; set; }
    DbSet<Bid> Bid { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
