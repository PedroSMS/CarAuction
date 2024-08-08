using CarAuction.Application.Commands.CreateAuction;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using CarAuction.IntegrationTests.Fixtures;
using CarAuction.IntegrationTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarAuction.IntegrationTests.Application.Controllers;

[Collection(nameof(CustomApplicationFactoryCollection))]
public class AuctionControllerTests
{
    private const string Endpoint = "api/auctions";
    private readonly CustomApplicationFactory _factory;
    private readonly HttpClient _httpClient;
    private readonly ICarAuctionContext _db;

    public AuctionControllerTests(CustomApplicationFactory factory)
    {
        _factory = factory;
        _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = true });
        _db = factory.GetRequiredService<ICarAuctionContext>();
    }

    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenVehicleIsAlreadyInAnActiveAuction()
    {
        // Arrange
        var vehicleId = await SeedDatabaseWithTruckInAnActiveAuction();
        var request = GetRequest(vehicleId);

        // Act
        var response = await _httpClient.PostAsJsonAsync(Endpoint, request);
        var responseContent = JsonSerializer.Deserialize<ProblemDetails>(
            await response.Content.ReadAsStreamAsync(), JsonSerializerHelper.ReadOptions);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        responseContent.Should().NotBeNull();
        responseContent!.Status.Should().Be(StatusCodes.Status400BadRequest);
    }

    [Fact]
    public async Task Create_ShouldReturnNotFound_WhenVehicleIsNotInTheDatabase()
    {
        // Arrange
        var request = GetRequest(Guid.NewGuid());

        // Act
        var response = await _httpClient.PostAsJsonAsync(Endpoint, request);
        var responseContent = JsonSerializer.Deserialize<ProblemDetails>(
            await response.Content.ReadAsStreamAsync(), JsonSerializerHelper.ReadOptions);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        responseContent.Should().NotBeNull();
        responseContent!.Status.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Create_ShouldCreateNewAuction_WhenRequestIsValid()
    {
        // Arrange
        var vehicleId = await SeedDatabaseWithTruck();
        var request = GetRequest(vehicleId);

        // Act
        var response = await _httpClient.PostAsJsonAsync(Endpoint, request);
        var insertedAuction = JsonSerializer.Deserialize<Auction>(
            await response.Content.ReadAsStreamAsync(), JsonSerializerHelper.ReadOptions);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        insertedAuction.Should().NotBeNull();
        insertedAuction!.VehicleId.Should().Be(vehicleId);
    }

    [Fact]
    public async Task Close_ShouldReturnNotFound_WhenAuctionIsNotInDatabaseOrItIsAlreadyClosed()
    {
        // Arrange
        var auctionId = await SeedDatabaseWithAuction(true);

        // Act
        var response = await _httpClient.PutAsync($"{Endpoint}/{auctionId}/close", null);
        var responseContent = JsonSerializer.Deserialize<ProblemDetails>(
            await response.Content.ReadAsStreamAsync(), JsonSerializerHelper.ReadOptions);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
        responseContent.Should().NotBeNull();
        responseContent!.Status.Should().Be(StatusCodes.Status404NotFound);
    }

    [Fact]
    public async Task Close_ShouldCloseAnAuction_WhenRequestIsValid()
    {
        // Arrange
        var auctionId = await SeedDatabaseWithAuction();

        // Act
        var response = await _httpClient.PutAsync($"{Endpoint}/{auctionId}/close", null);
        var closedAuction = await FetchUpdatedAuctionAsync(auctionId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        closedAuction.Should().NotBeNull();
        closedAuction!.FinishedAtUtc.Should().NotBeNull();
    }

    #region private
    private async Task<Guid> SeedDatabaseWithTruck()
    {
        var truck = DatabaseSeederHelper.GetTrucks(1).First();

        _db.Vehicle.Add(truck);
        await _db.SaveChangesAsync();

        return truck.Id;
    }

    private async Task<Guid> SeedDatabaseWithTruckInAnActiveAuction()
    {
        var truck = DatabaseSeederHelper.GetTrucks(1).First();
        var auction = DatabaseSeederHelper.GetAuctionsForVehicle(1, truck.Id).First();

        _db.Vehicle.Add(truck);
        _db.Auction.Add(auction);
        await _db.SaveChangesAsync();

        return truck.Id;
    }

    private static CreateAuctionCommandRequest GetRequest(Guid carId)
    {
        return new()
        {
            VehicleId = carId
        };
    }

    private async Task<Guid> SeedDatabaseWithAuction(bool isFinished = false)
    {
        var truck = DatabaseSeederHelper
            .GetTrucks(1)
            .First();
        var auction = DatabaseSeederHelper
            .GetAuctionsForVehicle(1, truck.Id, isFinished)
            .First();
        
        _db.Vehicle.Add(truck);
        _db.Auction.Add(auction);

        await _db.SaveChangesAsync();

        return auction.Id;
    }

    private async Task<Auction?> FetchUpdatedAuctionAsync(Guid auctionId)
    {
        var newDb = _factory.GetRequiredService<ICarAuctionContext>();
        return await newDb.Auction.FirstOrDefaultAsync(e => e.Id == auctionId);
    }
    #endregion
}
