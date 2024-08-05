using CarAuction.Application.Common.Interfaces;
using CarAuction.Application.Queries.GetCars;
using CarAuction.IntegrationTests.Fixtures;
using CarAuction.IntegrationTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;


namespace CarAuction.IntegrationTests.Application.Controllers;

[Collection(nameof(CustomApplicationFactoryCollection))]
public class CarControllerTests
{
    private readonly HttpClient _httpClient;
    private readonly ICarAuctionContext _db;

    public CarControllerTests(CustomApplicationFactory factory)
    {
        _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions { AllowAutoRedirect = true });
        _db = factory.GetRequiredService<ICarAuctionContext>();
    }

    [Fact]
    public async Task Get_ShouldReturnOnlyReturnHatchback_WhenRequestHasTypeIdSetToOne()
    {
        // Arrange
        await SeedDatabase();

        // Act
        var result = await _httpClient.GetFromJsonAsync<List<GetCarsQueryResponse>>("api/cars?typeId=1");

        // Assert
        result.Should().NotBeNullOrEmpty();
        result!.Count.Should().Be(4);
    }

    private async Task SeedDatabase()
    {
        _db.Vehicle.AddRange(DatabaseSeederHelper.GetHatchbacks(4));
        _db.Vehicle.AddRange(DatabaseSeederHelper.GetSedans(6));

        await _db.SaveChangesAsync();
    }
}
