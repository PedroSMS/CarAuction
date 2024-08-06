using CarAuction.Application.Commands.CreateCar;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using CarAuction.Domain.Enums;
using FluentAssertions;
using MockQueryable.Moq;
using Moq;

namespace CarAuction.UnitTests.Application.Commands.CreateCar;

public class CreateCarCommandValidatorTests
{
    private CreateCarCommandValidator _sut;
    private Mock<ICarAuctionContext> _mockDb = new Mock<ICarAuctionContext>();

    public CreateCarCommandValidatorTests()
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
        var result = await _sut.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnInvalidResult_WhenYearIsLessThan1885AndStartBidIs0()
    {
        // Arrange
        var command = GetCommandForYearAndStartBidValidationErrors();
        UpdateMockDb();

        // Act
        var result = await _sut.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnInvalidResult_WhenTypeIsTruckAndDontHaveLoadCapacityPropertySet()
    {
        // Arrange
        var command = GetCommandForTruckValidationError();
        UpdateMockDb();

        // Act
        var result = await _sut.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
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

    private static CreateCarCommand GetCommandForIdentifierValidationError(string identifier)
    {
        var request = new CreateCarCommandRequest
        {
            TypeId = (int)ECarType.Hatchback,
            StartingBid = 10000,
            Manufacturer = "Opel",
            Model = "Corsa",
            Year = 1990,
            NumberOfDoors = 5,
            Identifier = identifier
        };

        return request.ToCommand();
    }

    private static CreateCarCommand GetCommandForYearAndStartBidValidationErrors()
    {
        var request = new CreateCarCommandRequest
        {
            TypeId = (int)ECarType.Hatchback,
            StartingBid = 0,
            Manufacturer = "Opel",
            Model = "Corsa",
            Year = 1700,
            NumberOfDoors = 5,
            Identifier = Guid.NewGuid().ToString()
        };

        return request.ToCommand();
    }

    private static CreateCarCommand GetCommandForTruckValidationError()
    {
        var request = new CreateCarCommandRequest
        {
            TypeId = (int)ECarType.Truck,
            StartingBid = 10000,
            Manufacturer = "Volvo",
            Model = "FE",
            Year = 1990,
            Identifier = Guid.NewGuid().ToString()
        };

        return request.ToCommand();
    }

    private static CreateCarCommand GetCommandForValidResult()
    {
        var request = new CreateCarCommandRequest
        {
            TypeId = (int)ECarType.Suv,
            StartingBid = 10000,
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
