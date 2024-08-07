using CarAuction.Application.Commands.CreateAuction;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using FluentAssertions;
using FluentValidation.TestHelper;
using MockQueryable.Moq;
using Moq;

namespace CarAuction.UnitTests.Application.Commands.CreateAuction;

public class CreateAuctionCommandValidatorTests
{
    private readonly CreateAuctionCommandValidator _sut;
    private readonly Mock<ICarAuctionContext> _mockDb = new();

    public CreateAuctionCommandValidatorTests()
    {
        _sut = new(_mockDb.Object);
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnInvalidResult_WhenCarDoesNotExistsInDatabase()
    {
        // Arrange
        var command = GetCommand();
        UpdateMockDb();

        // Act
        var result = await _sut.TestValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(e => e.CarId)
            .WithErrorMessage("Car does not exists in database.");
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnInvalidResult_WhenCarIsAlreadyInAnActiveAuction()
    {
        // Arrange
        var carId = Guid.NewGuid();
        var command = GetCommand(carId);
        UpdateMockDb(carId);

        // Act
        var result = await _sut.TestValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(e => e.CarId)
            .WithErrorMessage("Car is already in an active auction.");
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnValidResult_WhenCarIsInDatabaseAndDoNotExistsInAnActiveAuction()
    {
        // Arrange
        var carId = Guid.NewGuid();
        var command = GetCommand(carId);
        UpdateMockDb(carId, Guid.NewGuid());

        // Act
        var result = await _sut.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    #region private
    private void UpdateMockDb(Guid? carId = null, Guid? auctionCarId = null)
    {
        var cars = new List<Vehicle>
        {
            new Sedan 
            { 
                Id = carId ?? Guid.NewGuid(), 
                Identifier = Guid.NewGuid().ToString(), 
                Manufacturer = "Audi", 
                Model = "A4"
            }
        };

        _mockDb.Setup(e => e.Vehicle)
            .Returns(cars.AsQueryable().BuildMockDbSet().Object);

        var auctions = new List<Auction>
        {
            new() 
            {
                CarId = auctionCarId ?? cars[0].Id,
                Id = Guid.NewGuid()
            }
        };

        _mockDb.Setup(e => e.Auction)
            .Returns(auctions.AsQueryable().BuildMockDbSet().Object);
    }

    private static CreateAuctionCommand GetCommand(Guid? carId = null)
    {
        var request = new CreateAuctionCommandRequest
        {
            CarId = carId ?? Guid.NewGuid(),
        };

        return request.ToCommand();
    }
    #endregion
}
