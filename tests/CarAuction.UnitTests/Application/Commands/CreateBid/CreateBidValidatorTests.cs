using CarAuction.Application.Commands.CreateBid;
using CarAuction.Application.Common.Interfaces;
using CarAuction.Domain.Entities;
using FluentAssertions;
using FluentValidation.TestHelper;
using MockQueryable.Moq;
using Moq;

namespace CarAuction.UnitTests.Application.Commands.CreateBid;

public class CreateBidValidatorTests
{
    private readonly CreateBidCommandValidator _sut;
    private readonly Mock<ICarAuctionContext> _mockDb = new();

    public CreateBidValidatorTests()
    {
        _sut = new(_mockDb.Object);
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnInvalidResult_WhenMinimunBidValueOfTheCarIsHigerThanRequestBidValue()
    {
        // Arrange
        var auctionId = Guid.NewGuid();
        var command = GetCommand(auctionId);
        UpdateMockDb(auctionId);

        // Act
        var result = await _sut.TestValidateAsync(command);

        // Assert
        result.IsValid.Should().BeFalse();
        result.ShouldHaveValidationErrorFor(e => e.Value)
            .WithErrorMessage("Must place a bid higher than '1500'.");
    }

    [Fact]
    public async Task ValidateAsync_ShouldReturnValidResult_WhenRequestBidValueIsHigerThanMinimunBidValueOfTheCar()
    {
        // Arrange
        var auctionId = Guid.NewGuid();
        var command = GetCommand(auctionId, 2000);
        UpdateMockDb(auctionId);

        // Act
        var result = await _sut.TestValidateAsync(command);

        // Assert
        result.IsValid.Should().BeTrue();
    }

    #region private
    private void UpdateMockDb(Guid auctionId)
    {
        var vehicles = new List<Vehicle>
        {
            new Sedan
            {
                Id = Guid.NewGuid(),
                Identifier = Guid.NewGuid().ToString(),
                Manufacturer = "Audi",
                Model = "A4",
                OpeningBid = 1000
            }
        };

        _mockDb.Setup(e => e.Vehicle)
            .Returns(vehicles.AsQueryable().BuildMockDbSet().Object);

        var auctions = new List<Auction>
        {
            new()
            {
                Id = auctionId,
                VehicleId = vehicles[0].Id,
                Vehicle = vehicles[0],
                Bids =
                [
                    new() { AuctionId = auctionId, Value = 1500 }
                ]
            }
        };

        _mockDb.Setup(e => e.Auction)
            .Returns(auctions.AsQueryable().BuildMockDbSet().Object);
    }

    private static CreateBidCommand GetCommand(Guid? auctionId = null, decimal? value = null)
    {
        var request = new CreateBidCommandRequest
        {
            AuctionId = auctionId ?? Guid.NewGuid(),
            Value = value ?? 1000
        };

        return request.ToCommand();
    }
    #endregion

}
