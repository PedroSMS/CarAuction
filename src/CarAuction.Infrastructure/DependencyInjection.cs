using CarAuction.Application.Common.Interfaces;
using CarAuction.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CarAuction.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<CarAuctionContext>(options =>
        {
            options.UseInMemoryDatabase("InMemoryDb");
            //options.EnableDetailedErrors();
            //options.EnableSensitiveDataLogging();
            //options.UseSqlServer(configuration.GetConnectionString("SqlServer"),
            //    sqlOptions =>
            //    {
            //        sqlOptions.EnableRetryOnFailure(5, TimeSpan.FromSeconds(15), null);
            //    });
        });

        services.AddScoped<ICarAuctionContext>(provider =>
            provider.GetRequiredService<CarAuctionContext>());


        return services;
    }
}
