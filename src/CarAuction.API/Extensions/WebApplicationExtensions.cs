using CarAuction.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CarAuction.API.Extensions;

public static class WebApplicationExtensions
{
    public static void EnsureDatabaseIsUpToDate(this WebApplication application)
    {
        using var scope = application.Services.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<CarAuctionContext>();

        db.Database.Migrate();
    }
}
