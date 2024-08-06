using CarAuction.Application.Commands.CreateAuction;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using CarAuction.IntegrationTests.Fixtures;
using CarAuction.IntegrationTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace CarAuction.IntegrationTests.Application.Controllers;

[Collection(nameof(CustomApplicationFactoryCollection))]
public class AuctionControllerTests
{
    private readonly HttpClient _httpClient;
    private readonly ICarAuctionContext _db;

    public AuctionControllerTests(CustomApplicationFactory factory)
    {
        _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = true });
        _db = factory.GetRequiredService<ICarAuctionContext>();
    }

    [Fact]
    public async Task Create_ShouldCreateNewAuction_WhenRequestIsValid()
    {
        // Arrange
        var carId = await SeedDatabase();
        var request = GetRequest(carId);

        // Act
        var response = await _httpClient.PostAsJsonAsync("api/auctions", request);
        var insertedAuction = JsonSerializer.Deserialize<Auction>(
            await response.Content.ReadAsStreamAsync(), JsonSerializerOptionsHelper.ReadOptions);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        insertedAuction.Should().NotBeNull();
        insertedAuction!.CarId.Should().Be(carId);
    }


    #region private
    private async Task<Guid> SeedDatabase()
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

    #endregion
}
