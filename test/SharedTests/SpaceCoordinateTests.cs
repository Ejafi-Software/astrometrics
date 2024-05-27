using Ejafi.Astrometrics.Shared;
using FluentAssertions;

namespace SharedTests;

public class SpaceCoordinateTests
{
    [Fact]
    public void Equals_WithIdenticalCoordinates_ReturnsTrue()
    {
        // Arrange
        var galactic = new Vector3<long>(1, 2, 3);
        var local = new Vector3<long>(4, 5, 6);
        var coordinate1 = new SpaceCoordinate(galactic, local);
        var coordinate2 = new SpaceCoordinate(galactic, local);

        // Act
        var result = coordinate1.Equals(coordinate2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Equals_WithDifferentCoordinates_ReturnsFalse()
    {
        // Arrange
        var coordinate1 = new SpaceCoordinate(new Vector3<long>(1, 2, 3), new Vector3<long>(4, 5, 6));
        var coordinate2 = new SpaceCoordinate(new Vector3<long>(7, 8, 9), new Vector3<long>(10, 11, 12));

        // Act
        var result = coordinate1.Equals(coordinate2);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void GetHashCode_WithIdenticalCoordinates_ReturnsSameHashCode()
    {
        // Arrange
        var galactic = new Vector3<long>(1, 2, 3);
        var local = new Vector3<long>(4, 5, 6);
        var coordinate1 = new SpaceCoordinate(galactic, local);
        var coordinate2 = new SpaceCoordinate(galactic, local);

        // Act
        var hashCode1 = coordinate1.GetHashCode();
        var hashCode2 = coordinate2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void GetHashCode_WithDifferentCoordinates_ReturnsDifferentHashCodes()
    {
        // Arrange
        var coordinate1 = new SpaceCoordinate(new Vector3<long>(1, 2, 3), new Vector3<long>(4, 5, 6));
        var coordinate2 = new SpaceCoordinate(new Vector3<long>(7, 8, 9), new Vector3<long>(10, 11, 12));

        // Act
        var hashCode1 = coordinate1.GetHashCode();
        var hashCode2 = coordinate2.GetHashCode();

        // Assert
        hashCode1.Should().NotBe(hashCode2);
    }

    [Fact]
    public void Operator_Equal_WithIdenticalCoordinates_ReturnsTrue()
    {
        // Arrange
        var galactic = new Vector3<long>(1, 2, 3);
        var local = new Vector3<long>(4, 5, 6);
        var coordinate1 = new SpaceCoordinate(galactic, local);
        var coordinate2 = new SpaceCoordinate(galactic, local);

        // Act
        var result = coordinate1 == coordinate2;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Operator_NotEqual_WithDifferentCoordinates_ReturnsTrue()
    {
        // Arrange
        var coordinate1 = new SpaceCoordinate(new Vector3<long>(1, 2, 3), new Vector3<long>(4, 5, 6));
        var coordinate2 = new SpaceCoordinate(new Vector3<long>(7, 8, 9), new Vector3<long>(10, 11, 12));

        // Act
        var result = coordinate1 != coordinate2;

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void CalculateDistance_WithIdenticalCoordinates_ReturnsZeroDistance()
    {
        // Arrange
        var coordinate1 = new SpaceCoordinate(new Vector3<long>(1, 2, 3), new Vector3<long>(4, 5, 6));
        var coordinate2 = new SpaceCoordinate(new Vector3<long>(1, 2, 3), new Vector3<long>(4, 5, 6));

        // Act
        var distance = coordinate1.CalculateDistance(coordinate2);

        // Assert
        distance.Magnitude.Should().Be(0);
        distance.Units.Should().Be("KM");
    }

    [Fact]
    public void CalculateDistance_WithDifferentCoordinates_ReturnsCorrectDistance()
    {
        // Arrange
        var coordinate1 = new SpaceCoordinate(new Vector3<long>(1, 2, 3), new Vector3<long>(4, 5, 6));
        var coordinate2 = new SpaceCoordinate(new Vector3<long>(7, 8, 9), new Vector3<long>(10, 11, 12));
        var expectedDistance = Math.Sqrt(
            Math.Pow(7 - 1, 2) * Math.Pow(9.461e12, 2) +
            Math.Pow(8 - 2, 2) * Math.Pow(9.461e12, 2) +
            Math.Pow(9 - 3, 2) * Math.Pow(9.461e12, 2) +
            Math.Pow(10 - 4, 2) +
            Math.Pow(11 - 5, 2) +
            Math.Pow(12 - 6, 2)
        );

        // Act
        var distance = coordinate1.CalculateDistance(coordinate2);

        // Assert
        distance.Magnitude.Should().BeApproximately(expectedDistance / 9.461e12, 1e-6);
        distance.Units.Should().Be("LY");
    }

    [Fact]
    public void ZeroProperty_ReturnsVectorWithDefaultComponents()
    {
        // Arrange
        var expectedGalactic = Vector3<long>.Zero;
        var expectedLocal = Vector3<long>.Zero;

        // Act
        var zeroCoordinate = SpaceCoordinate.Zero;

        // Assert
        zeroCoordinate.Galactic.Should().BeEquivalentTo(expectedGalactic);
        zeroCoordinate.Local.Should().BeEquivalentTo(expectedLocal);
    }
}
