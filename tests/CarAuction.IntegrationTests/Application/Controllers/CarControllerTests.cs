using CarAuction.Application.Commands.CreateVehicle;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Application.Queries.GetVehicles;
using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;
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
public class CarControllerTests
{
    private const string Endpoint = "api/vehicles";
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
        var result = await _httpClient.GetFromJsonAsync<List<GetVehiclesQueryResponse>>($"{Endpoint}?typeId=1");

        // Assert
        result.Should().NotBeNullOrEmpty();
        result!.Count.Should().Be(4);
    }

    [Fact]
    public async Task Create_ShouldReturnBadRequest_WhenTypeIsHatchbackAndDoesNotHaveNumberOfDoorsSet()
    {
        // Arrange
        var request = GetInvalidRequest();

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
    public async Task Create_ShouldCreateNewVehicle_WhenRequestIsValid()
    {
        // Arrange
        var request = GetRequest();

        // Act
        var response = await _httpClient.PostAsJsonAsync(Endpoint, request);
        var insertedVehicle = JsonSerializer.Deserialize<Truck>(
            await response.Content.ReadAsStreamAsync(), JsonSerializerHelper.ReadOptions);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        insertedVehicle.Should().NotBeNull();
        insertedVehicle!.Manufacturer.Should().Be(request.Manufacturer);
        insertedVehicle!.Model.Should().Be(request.Model);
        insertedVehicle!.OpeningBid.Should().Be(request.OpeningBid);
        insertedVehicle.LoadCapacity.Should().Be(request.LoadCapacity);
    }

    #region private
    private async Task SeedDatabase()
    {
        _db.Vehicle.AddRange(DatabaseSeederHelper.GetHatchbacks(4));
        _db.Vehicle.AddRange(DatabaseSeederHelper.GetSedans(6));

        await _db.SaveChangesAsync();
    }

    private CreateVehicleCommandRequest GetRequest()
    {
        return new()
        {
            Identifier = Guid.NewGuid().ToString(),
            LoadCapacity = 15000,
            Manufacturer = "Volvo",
            Model = "FE",
            TypeId = (int)EVehicleType.Truck,
            Year = 2000,
            OpeningBid = 25000
        };
    }

    private CreateVehicleCommandRequest GetInvalidRequest()
    {
        return new()
        {
            Identifier = Guid.NewGuid().ToString(),
            Manufacturer = "Volvo",
            Model = "FE",
            TypeId = (int)EVehicleType.Hatchback,
            Year = 2000,
            OpeningBid = 25000
        };
    }

    #endregion
}
