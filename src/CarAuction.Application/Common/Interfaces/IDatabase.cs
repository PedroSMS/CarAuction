using CarAuction.Domain.Entities;

namespace CarAuction.Application.Common.Interfaces;

public interface IDatabase
{
    ICollection<Vehicle> Cars { get; }
    ICollection<Auction> Auctions { get; }
    ICollection<User> Users { get; }
}
