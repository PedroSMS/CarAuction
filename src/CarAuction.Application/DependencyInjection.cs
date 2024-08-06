using CarAuction.Application.Commands.CreateAuction;
using CarAuction.Application.Commands.CreateBid;
using CarAuction.Application.Commands.CreateCar;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CarAuction.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this  IServiceCollection services)
    {
        services.AddMediatR(e => e.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssemblyContaining<CreateCarCommandValidator>();

        services.AddScoped<ICreateCarCommandAdapter, CreateCarCommandAdapter>();
        services.AddScoped<ICreateAuctionCommandAdapter, CreateAuctionCommandAdapter>();
        services.AddScoped<ICreateBidCommandAdapter, CreateBidCommandAdapter>();

        return services;
    }
}
