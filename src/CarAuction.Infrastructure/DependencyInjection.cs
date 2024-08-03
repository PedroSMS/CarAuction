using CarAuction.Application.Common.Interfaces;
using CarAuction.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace CarAuction.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IDatabase, Database>();

        return services;
    }
}
