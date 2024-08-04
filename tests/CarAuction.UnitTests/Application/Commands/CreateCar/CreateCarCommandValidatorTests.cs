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

    [Fact]
    public async Task ValidateAsync_ShouldReturnIvalidadeResult_WhenIdentifierAlreadyExistsInDatabaseAsync()
    {
        // Arrange
        const string identifier = "SameIdentifier!";
        UpdateMockDb(identifier);
        var command = GetCommand(identifier);
        _sut = new CreateCarCommandValidator(_mockDb.Object);

        // Act
        var result = await _sut.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
    }

    private void UpdateMockDb(string identifier)
    {
        var data = new Vehicle[] { new Sedan { Identifier = identifier, Manufacturer = "Toyota" } };

        _mockDb.Setup(e => e.Vehicle)
            .Returns(data.AsQueryable().BuildMockDbSet().Object);
    }

    private static CreateCarCommand GetCommand(string identifier)
    {
        var request = new CreateCarCommandRequest
        {
            TypeId = (int)ECarType.Hatchback,
            StartingBid = 10000,
            Manufacturer = "Opel",
            Year = 1990,
            NumberOfDoors = 5,
            Identifier = identifier
        };

        return new(request);
    }
}
