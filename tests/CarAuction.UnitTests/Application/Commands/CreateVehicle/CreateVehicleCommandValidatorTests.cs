using CarAuction.Application.Commands.CreateVehicle;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;
using FluentAssertions;
using FluentValidation.TestHelper;
using MockQueryable.Moq;
using Moq;

namespace CarAuction.UnitTests.Application.Commands.CreateVehicle;

public class CreateVehicleCommandValidatorTests
{
    private readonly CreateVehicleCommandValidator _sut;
    private readonly Mock<ICarAuctionContext> _mockDb = new();

    public CreateVehicleCommandValidatorTests()
    {
        _sut = new(_mockDb.Object);
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnInvalidResult_WhenIdentifierAlreadyExistsInDatabase()
    {
        // Arrange
        const string identifier = "SameIdentifier!";
        var command = GetCommandForIdentifierValidationError(identifier);
        UpdateMockDb(identifier);

        // Act
        var result = await _sut.TestValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(e => e.Identifier)
            .WithErrorMessage("Identifier already exists in the database.");
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnInvalidResult_WhenYearIsLessThan1885AndStartBidIs0()
    {
        // Arrange
        var command = GetCommandForYearAndStartBidValidationErrors();
        UpdateMockDb();

        // Act
        var result = await _sut.TestValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(e => e.Year)
            .WithErrorMessage($"'Year' must be between 1885 and {DateTime.UtcNow.Year}. You entered 1700.");
        result.ShouldHaveValidationErrorFor(e => e.OpeningBid)
            .WithErrorMessage("'Opening Bid' must not be empty.");
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnInvalidResult_WhenTypeIsTruckAndDontHaveLoadCapacityPropertySet()
    {
        // Arrange
        var command = GetCommandForTruckValidationError();
        UpdateMockDb();

        // Act
        var result = await _sut.TestValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(e => e.LoadCapacity)
            .WithErrorMessage("'Load Capacity' must not be empty.");
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnValidResult_WhenHaveAllSuvPropertiesRequiredSet()
    {
        // Arrange
        var command = GetCommandForValidResult();
        UpdateMockDb();

        // Act
        var result = await _sut.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    #region private
    private void UpdateMockDb(string? identifier = null)
    {
        var data = new List<Vehicle>();
        
        if (string.IsNullOrWhiteSpace(identifier) is false)
        {
            data.Add(new Sedan { Identifier = identifier, Manufacturer = "Toyota", Model = "Yaris" });
        };

        _mockDb.Setup(e => e.Vehicle)
            .Returns(data.AsQueryable().BuildMockDbSet().Object);
    }

    private static CreateVehicleCommand GetCommandForIdentifierValidationError(string identifier)
    {
        var request = new CreateVehicleCommandRequest
        {
            TypeId = (int)EVehicleType.Hatchback,
            OpeningBid = 10000,
            Manufacturer = "Opel",
            Model = "Corsa",
            Year = 1990,
            NumberOfDoors = 5,
            Identifier = identifier
        };

        return request.ToCommand();
    }

    private static CreateVehicleCommand GetCommandForYearAndStartBidValidationErrors()
    {
        var request = new CreateVehicleCommandRequest
        {
            TypeId = (int)EVehicleType.Hatchback,
            OpeningBid = 0,
            Manufacturer = "Opel",
            Model = "Corsa",
            Year = 1700,
            NumberOfDoors = 5,
            Identifier = Guid.NewGuid().ToString()
        };

        return request.ToCommand();
    }

    private static CreateVehicleCommand GetCommandForTruckValidationError()
    {
        var request = new CreateVehicleCommandRequest
        {
            TypeId = (int)EVehicleType.Truck,
            OpeningBid = 10000,
            Manufacturer = "Volvo",
            Model = "FE",
            Year = 1990,
            Identifier = Guid.NewGuid().ToString()
        };

        return request.ToCommand();
    }

    private static CreateVehicleCommand GetCommandForValidResult()
    {
        var request = new CreateVehicleCommandRequest
        {
            TypeId = (int)EVehicleType.Suv,
            OpeningBid = 10000,
            Manufacturer = "BMW",
            Model = "X5",
            Year = 1990,
            NumberOfSeats = 9,
            Identifier = Guid.NewGuid().ToString()
        };

        return request.ToCommand();
    }
    #endregion
}