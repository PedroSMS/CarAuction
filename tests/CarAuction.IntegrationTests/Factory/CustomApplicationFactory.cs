using CarAuction.API;
using CarAuction.Infrastructure.Persistence.Contexts;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace CarAuction.IntegrationTests.Fixtures;

public class CustomApplicationFactory : WebApplicationFactory<IApiMarker>//, IAsyncLifetime
{

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        base.ConfigureWebHost(builder);

        builder.ConfigureTestServices(services =>
        {
            services.Remove(services
                .Single(e => e.ServiceType == typeof(DbContextOptions<CarAuctionContext>)));

            services.AddDbContext<CarAuctionContext>(options =>
                options.UseInMemoryDatabase("inMemoryDb"));

            using var scope = services.BuildServiceProvider().CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<CarAuctionContext>();

            db.Database.EnsureCreated();
        });
    }

    public T GetRequiredService<T>() where T : notnull
    {
        var scope = Services.CreateScope();
        return scope.ServiceProvider.GetRequiredService<T>();
    }
}

[CollectionDefinition(nameof(CustomApplicationFactoryCollection))]
public class CustomApplicationFactoryCollection : ICollectionFixture<CustomApplicationFactory>
{
    // This class has no code, and is never created. Its purpose is simply
    // to be the place to apply [CollectionDefinition] and all the
    // ICollectionFixture<> interfaces.
}
