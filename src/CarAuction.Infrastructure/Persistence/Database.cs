using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;

namespace CarAuction.Infrastructure.Persistence;

public class Database : IDatabase
{
    public ICollection<Vehicle> Cars { get; } = [];
    public ICollection<Auction> Auctions { get; } = [];
    public ICollection<User> Users { get; } = [];
}