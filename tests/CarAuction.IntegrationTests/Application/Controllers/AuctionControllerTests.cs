using CarAuction.Application.Commands.CreateAuction;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using CarAuction.IntegrationTests.Fixtures;
using CarAuction.IntegrationTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarAuction.IntegrationTests.Application.Controllers;

[Collection(nameof(CustomApplicationFactoryCollection))]
public class AuctionControllerTests
{
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
    public async Task Create_ShouldCreateNewAuction_WhenRequestIsValid()
    {
        // Arrange
        var carId = await SeedDatabaseWithTruck();
        var request = GetRequest(carId);

        // Act
        var response = await _httpClient.PostAsJsonAsync("api/auctions", request);
        var insertedAuction = JsonSerializer.Deserialize<Auction>(
            await response.Content.ReadAsStreamAsync(), JsonSerializerHelper.ReadOptions);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        insertedAuction.Should().NotBeNull();
        insertedAuction!.CarId.Should().Be(carId);
    }

    [Fact]
    public async Task Close_ShouldCloseAnAuction_WhenRequestIsValid()
    {
        // Arrange
        var db = _factory.GetRequiredService<ICarAuctionContext>();
        var auctionId = await SeedDatabaseWithAuction();

        // Act
        var response = await _httpClient.PutAsync($"api/auctions/{auctionId}/close", null);
        var closedAuction = await FetchUpdatedAuctionAsync(auctionId);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NoContent);
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

    private static CreateAuctionCommandRequest GetRequest(Guid carId)
    {
        return new()
        {
            CarId = carId
        };
    }

    private async Task<Guid> SeedDatabaseWithAuction()
    {
        var truck = DatabaseSeederHelper.GetTrucks(1).First();
        var auction = DatabaseSeederHelper.GetAuctionsForCar(1, truck.Id).First();
        
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
