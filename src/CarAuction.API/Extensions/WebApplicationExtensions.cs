using CarAuction.Infrastructure.Persistence.Contexts;

namespace CarAuction.API.Extensions;

public static class WebApplicationExtensions
{
    public static void EnsureDatabaseIsUpToDate(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CarAuctionContext>();

        db.Database.EnsureCreated();
    }
}
