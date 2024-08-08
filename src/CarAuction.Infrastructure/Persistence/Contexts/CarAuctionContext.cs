using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CarAuction.Infrastructure.Persistence.Contexts;

public class CarAuctionContext(DbContextOptions<CarAuctionContext> options) : DbContext(options), ICarAuctionContext
{
    public DbSet<Vehicle> Vehicle { get; set; }
    public DbSet<Hatchback> HatchBack { get; set; }
    public DbSet<Sedan> Sedan { get; set; }
    public DbSet<Suv> Suv { get; set; }
    public DbSet<Truck> Truck { get; set; }

    public DbSet<Auction> Auction { get; set; }
    public DbSet<Bid> Bid { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}