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
    public async Task ValidateAsync_ShouldReturnInvalidResult_WhenCarIsAlreadyInAnActiveAuction()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var command = GetCommand(vehicleId);
        UpdateMockDb(vehicleId);

        // Act
        var result = await _sut.TestValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(e => e.VehicleId)
            .WithErrorMessage("Vehicle is already in an active auction.");
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnValidResult_WhenCarIsInDatabaseAndDoNotExistsInAnActiveAuction()
    {
        // Arrange
        var vehicleId = Guid.NewGuid();
        var command = GetCommand(vehicleId);
        UpdateMockDb(vehicleId, Guid.NewGuid());

        // Act
        var result = await _sut.ValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    #region private
    private void UpdateMockDb(Guid? vehicleId = null, Guid? auctionCarId = null)
    {
        var vehicles = new List<Vehicle>
        {
            new Sedan 
            { 
                Id = vehicleId ?? Guid.NewGuid(), 
                Identifier = Guid.NewGuid().ToString(), 
                Manufacturer = "Audi", 
                Model = "A4"
            }
        };

        _mockDb.Setup(e => e.Vehicle)
            .Returns(vehicles.AsQueryable().BuildMockDbSet().Object);

        var auctions = new List<Auction>
        {
            new() 
            {
                VehicleId = auctionCarId ?? vehicles[0].Id,
                Id = Guid.NewGuid()
            }
        };

        _mockDb.Setup(e => e.Auction)
            .Returns(auctions.AsQueryable().BuildMockDbSet().Object);
    }

    private static CreateAuctionCommand GetCommand(Guid vehicleId)
    {
        var request = new CreateAuctionCommandRequest
        {
            VehicleId = vehicleId,
        };

        return request.ToCommand();
    }
    #endregion
}
