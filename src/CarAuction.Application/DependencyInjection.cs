using CarAuction.Application.Commands.CreateAuction;
using CarAuction.Application.Commands.CreateBid;
using CarAuction.Application.Commands.CreateVehicle;
using CarAuction.Application.Common.Behaviors;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CarAuction.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this  IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        services.AddScoped<ICreateVehicleCommandAdapter, CreateVehicleCommandAdapter>();
        services.AddScoped<ICreateAuctionCommandAdapter, CreateAuctionCommandAdapter>();
        services.AddScoped<ICreateBidCommandAdapter, CreateBidCommandAdapter>();

        return services;
    }
}
