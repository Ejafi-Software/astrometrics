using Ejafi.Astrometrics.Shared;
using FluentAssertions;

namespace SharedTests;

public class PointOfInterestTests
{
    [Fact]
    public void IsValid_WithNameLengthGreaterThanOrEqualToTwo_ReturnsTrue()
    {
        // Arrange
        var poi = new PointOfInterest { Name = "Sun" };

        // Act
        var result = poi.IsValid();

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsValid_WithNameLengthLessThanTwo_ReturnsFalse()
    {
        // Arrange
        var poi = new PointOfInterest { Name = "A" };

        // Act
        var result = poi.IsValid();

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_WithIdenticalCoordinates_ReturnsTrue()
    {
        // Arrange
        var coordinate = new SpaceCoordinate(new Vector3<long>(1, 2, 3), new Vector3<long>(4, 5, 6));
        var poi1 = new PointOfInterest { Name = "POI1", Coordinate = coordinate };
        var poi2 = new PointOfInterest { Name = "POI2", Coordinate = coordinate };

        // Act
        var result = poi1.Equals(poi2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_WithDifferentCoordinates_ReturnsFalse()
    {
        // Arrange
        var coordinate1 = new SpaceCoordinate(new Vector3<long>(1, 2, 3), new Vector3<long>(4, 5, 6));
        var coordinate2 = new SpaceCoordinate(new Vector3<long>(7, 8, 9), new Vector3<long>(10, 11, 12));
        var poi1 = new PointOfInterest { Name = "POI1", Coordinate = coordinate1 };
        var poi2 = new PointOfInterest { Name = "POI2", Coordinate = coordinate2 };

        // Act
        var result = poi1.Equals(poi2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_WithIdenticalCoordinates_ReturnsSameHashCode()
    {
        // Arrange
        var coordinate = new SpaceCoordinate(new Vector3<long>(1, 2, 3), new Vector3<long>(4, 5, 6));
        var poi1 = new PointOfInterest { Name = "POI1", Coordinate = coordinate };
        var poi2 = new PointOfInterest { Name = "POI2", Coordinate = coordinate };

        // Act
        var hashCode1 = poi1.GetHashCode();
        var hashCode2 = poi2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void GetHashCode_WithDifferentCoordinates_ReturnsDifferentHashCodes()
    {
        // Arrange
        var coordinate1 = new SpaceCoordinate(new Vector3<long>(1, 2, 3), new Vector3<long>(4, 5, 6));
        var coordinate2 = new SpaceCoordinate(new Vector3<long>(7, 8, 9), new Vector3<long>(10, 11, 12));
        var poi1 = new PointOfInterest { Name = "POI1", Coordinate = coordinate1 };
        var poi2 = new PointOfInterest { Name = "POI2", Coordinate = coordinate2 };

        // Act
        var hashCode1 = poi1.GetHashCode();
        var hashCode2 = poi2.GetHashCode();

        // Assert
        hashCode1.Should().NotBe(hashCode2);
    }

    [Fact]
    public void Operator_Equal_WithIdenticalCoordinates_ReturnsTrue()
    {
        // Arrange
        var coordinate = new SpaceCoordinate(new Vector3<long>(1, 2, 3), new Vector3<long>(4, 5, 6));
        var poi1 = new PointOfInterest { Name = "POI1", Coordinate = coordinate };
        var poi2 = new PointOfInterest { Name = "POI2", Coordinate = coordinate };

        // Act
        var result = poi1 == poi2;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Operator_NotEqual_WithDifferentCoordinates_ReturnsTrue()
    {
        // Arrange
        var coordinate1 = new SpaceCoordinate(new Vector3<long>(1, 2, 3), new Vector3<long>(4, 5, 6));
        var coordinate2 = new SpaceCoordinate(new Vector3<long>(7, 8, 9), new Vector3<long>(10, 11, 12));
        var poi1 = new PointOfInterest { Name = "POI1", Coordinate = coordinate1 };
        var poi2 = new PointOfInterest { Name = "POI2", Coordinate = coordinate2 };

        // Act
        var result = poi1 != poi2;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void SagittariusA_StaticProperty_ReturnsExpectedValues()
    {
        // Arrange
        const string expectedName = "Sagittarius A*";
        var expectedCoordinate = SpaceCoordinate.Zero;

        // Act
        var sagittariusA = PointOfInterest.SagittariusA;

        // Assert
        sagittariusA.Name.Should().Be(expectedName);
        sagittariusA.Coordinate.Should().Be(expectedCoordinate);
    }
}
