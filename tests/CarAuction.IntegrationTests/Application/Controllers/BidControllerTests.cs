using CarAuction.Application.Commands.CreateBid;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using CarAuction.IntegrationTests.Fixtures;
using CarAuction.IntegrationTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarAuction.IntegrationTests.Application.Controllers;

[Collection(nameof(CustomApplicationFactoryCollection))]
public class BidControllerTests
{
    private const string Endpoint = "api/bids";
    private readonly HttpClient _httpClient;
    private readonly ICarAuctionContext _db;

    public BidControllerTests(CustomApplicationFactory factory)
    {
        _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = true });
        _db = factory.GetRequiredService<ICarAuctionContext>();
    }

    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenBidIsLessThanCurrentBid()
    {
        // Arrange
        var auctionId = await SeedDatabaseWithAuction();
        var request = GetRequest(auctionId, 1);

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
    public async Task Create_ShouldCreateBidAuction_WhenRequestIsValidAnBidIsHigherThanCurrentOne()
    {
        // Arrange
        var auctionId = await SeedDatabaseWithAuction();
        var request = GetRequest(auctionId);

        // Act
        var response = await _httpClient.PostAsJsonAsync(Endpoint, request);
        var insertedBid = JsonSerializer.Deserialize<Bid>(
            await response.Content.ReadAsStreamAsync(), JsonSerializerHelper.ReadOptions);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        insertedBid.Should().NotBeNull();
        insertedBid!.AuctionId.Should().Be(request.AuctionId);
        insertedBid!.Value.Should().Be(request.Value);
    }

    #region private

    private async Task<Guid> SeedDatabaseWithAuction()
    {
        var truck = DatabaseSeederHelper.GetTrucks(1).First();
        var auction = DatabaseSeederHelper.GetAuctionsForVehicle(1, truck.Id).First();

        _db.Vehicle.Add(truck);
        _db.Auction.Add(auction);

        await _db.SaveChangesAsync();

        return auction.Id;
    }

    private CreateBidCommandRequest GetRequest(Guid auctionId, decimal? bidValue = null)
    {
        return new()
        {
            AuctionId = auctionId,
            Value = bidValue ?? 35000
        };
    }
    #endregion
}
