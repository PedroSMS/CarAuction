using CarAuction.Application.Commands.CreateVehicle;
using CarAuction.Domain.Entities;
using FluentAssertions;
using System.Collections;

namespace CarAuction.UnitTests.Application.Commands.CreateVehicle;

public class CreateVehicleCommandAdapterTests
{
    private readonly CreateVehicleCommandAdapter _sut = new();

    [Fact]
    public void GetVehicleFrom_ShouldThrowAnException_WhenTypeIdIsUnknown()
    {
        // Arrange
        var request = GetInvalidRequest();

        // Act
        Action act = () => _sut.GetVehicleFrom(request.ToCommand());

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>()
            .WithMessage("Specified argument was out of the range of valid values. (Parameter 'TypeId')");
    }

    [Theory]
    [ClassData(typeof(GetVehicleFromTestData))]
    public void GetVehicleFrom_ShouldReturnAVehicle_WhenTypeIdIsMatched(
        CreateVehicleCommandRequest request, Type expectedResult)
    {
        // Arrange
        var command = request.ToCommand();

        // Act
        var result = _sut.GetVehicleFrom(command);

        // Assert
        result.GetType().Should().Be(expectedResult); 
    }

    #region private 
    private CreateVehicleCommandRequest GetInvalidRequest()
    {
        return new()
        {
            TypeId = 10
        };
    }

    private class GetVehicleFromTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] 
            { 
                new CreateVehicleCommandRequest
                {
                    TypeId = 1,
                    Identifier = "NewHatchback",
                    Manufacturer = "Opel",
                    Model = "Corsa",
                    Year = 1999,
                    OpeningBid = 3000,
                    NumberOfDoors = 5
                },
                typeof(Hatchback)
            };
            yield return new object[]
            {
                new CreateVehicleCommandRequest
                {
                    TypeId = 2,
                    Identifier = "NewSedan",
                    Manufacturer = "Toyota",
                    Model = "Yaris",
                    Year = 2001,
                    OpeningBid = 3000,
                    NumberOfDoors = 5
                },
                typeof(Sedan)
            };
            yield return new object[]
            {
                new CreateVehicleCommandRequest
                {
                    TypeId = 3,
                    Identifier = "NewSuv",
                    Manufacturer = "BMW",
                    Model = "X5",
                    Year = 2011,
                    OpeningBid = 35000,
                    NumberOfSeats = 5
                },
                typeof(Suv)
            };
            yield return new object[]
            {
                new CreateVehicleCommandRequest
                {
                    TypeId = 4,
                    Identifier = "NewTruck",
                    Manufacturer = "Volvo",
                    Model = "FE",
                    Year = 2005,
                    OpeningBid = 45000,
                    LoadCapacity = 35000
                },
                typeof(Truck)
            };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    #endregion
}
