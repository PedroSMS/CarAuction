using CarAuction.Application.Commands.CreateCar;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Application.Queries.GetCars;
using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;
using CarAuction.IntegrationTests.Fixtures;
using CarAuction.IntegrationTests.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;


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

    [Fact]
    public async Task Create_ShouldCreateNewCar_WhenRequestIsValid()
    {
        // Arrange
        var request = GetRequest();

        // Act
        var response = await _httpClient.PostAsJsonAsync("api/cars", request);
        var insertedCar = JsonSerializer.Deserialize<Truck>(
            await response.Content.ReadAsStreamAsync(), JsonSerializerHelper.ReadOptions);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        insertedCar.Should().NotBeNull();
        insertedCar!.Manufacturer.Should().Be(request.Manufacturer);
        insertedCar!.Model.Should().Be(request.Model);
        insertedCar!.StartingBid.Should().Be(request.StartingBid);
        insertedCar.LoadCapacity.Should().Be(request.LoadCapacity);
    }

    #region private
    private async Task SeedDatabase()
    {
        _db.Vehicle.AddRange(DatabaseSeederHelper.GetHatchbacks(4));
        _db.Vehicle.AddRange(DatabaseSeederHelper.GetSedans(6));

        await _db.SaveChangesAsync();
    }

    private CreateCarCommandRequest GetRequest()
    {
        return new()
        {
            Identifier = Guid.NewGuid().ToString(),
            LoadCapacity = 15000,
            Manufacturer = "Volvo",
            Model = "FE",
            TypeId = (int)ECarType.Truck,
            Year = 2000,
            StartingBid = 25000
        };
    }

    #endregion
}
