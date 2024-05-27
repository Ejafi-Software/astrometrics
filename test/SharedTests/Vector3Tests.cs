using Ejafi.Astrometrics.Shared;
using FluentAssertions;

namespace SharedTests;

public class Vector3Tests
{
    [Fact]
    public void Equals_Method_WithIdenticalVectors_ReturnsTrue()
    {
        // Arrange
        var vector1 = new Vector3<int>(1, 2, 3);
        var vector2 = new Vector3<int>(1, 2, 3);

        // Act
        var result = vector1.Equals(vector2);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void StaticZeroProperty_ReturnsVectorWithDefaultComponents()
    {
        // Act
        var zeroVector = Vector3<int>.Zero;

        // Assert
        zeroVector.X.Should().Be(0);
        zeroVector.Y.Should().Be(0);
        zeroVector.Z.Should().Be(0);
    }

    [Fact]
    public void Distance_BetweenIdenticalVectors_ReturnsZero()
    {
        // Arrange
        var vector1 = new Vector3<int>(1, 2, 3);
        var vector2 = new Vector3<int>(1, 2, 3);

        // Act
        var distance = vector1.Distance(vector2);

        // Assert
        distance.Should().Be(0);
    }

    [Fact]
    public void CalculateDistance_BetweenTwoVectors_ReturnsCorrectDistance()
    {
        // Arrange
        var vector1 = new Vector3<int>(1, 2, 3);
        var vector2 = new Vector3<int>(4, 5, 6);
        var expectedDistance = Math.Sqrt(3 * 3 + 3 * 3 + 3 * 3);

        // Act
        var distance = vector1.Distance(vector2);

        // Assert
        distance.Should().Be(expectedDistance);
    }

    [Fact]
    public void GetHashCode_ReturnsSameValue_ForIdenticalVectors()
    {
        // Arrange
        var vector1 = new Vector3<int>(1, 2, 3);
        var vector2 = new Vector3<int>(1, 2, 3);

        // Act
        var hashCode1 = vector1.GetHashCode();
        var hashCode2 = vector2.GetHashCode();

        // Assert
        hashCode1.Should().Be(hashCode2);
    }

    [Fact]
    public void Vector3_ToString_ReturnsExpectedFormat()
    {
        // Arrange
        var vector = new Vector3<int>(1, 2, 3);

        // Act
        var result = vector.ToString();

        // Assert
        result.Should().Be("1, 2, 3");
    }

    [Fact]
    public void Equals_Method_WithNullObject_ReturnsFalse()
    {
        // Arrange
        var vector = new Vector3<int>(1, 2, 3);
        Vector3<int> nullVector = null!;

        // Act
        var result = vector.Equals(nullVector);

        // Assert
        result.Should().BeFalse();
    }

    [Fact]
    public void Equals_And_GetHashCode_WithExtremeValues()
    {
        // Arrange
        var vector1 = new Vector3<int>(int.MinValue, int.MinValue, int.MinValue);
        var vector2 = new Vector3<int>(int.MaxValue, int.MaxValue, int.MaxValue);

        // Act
        var areEqual = vector1.Equals(vector2);
        var hash1 = vector1.GetHashCode();
        var hash2 = vector2.GetHashCode();

        // Assert
        areEqual.Should().BeFalse();
        hash1.Should().NotBe(hash2);
    }

    [Fact]
    public void Operator_Equal_And_NotEqual()
    {
        // Arrange
        var vector1 = new Vector3<int>(1, 2, 3);
        var vector2 = new Vector3<int>(1, 2, 3);
        var vector3 = new Vector3<int>(4, 5, 6);

        // Act
        bool result1 = vector1 == vector2;
        bool result2 = vector1 != vector3;

        // Assert
        result1.Should().BeTrue();
        result2.Should().BeTrue();
    }

    [Fact]
    public void DistanceCalculation_WithNegativeComponents_ReturnsCorrectDistance()
    {
        // Arrange
        var vector1 = new Vector3<int>(-2, -3, -4);
        var vector2 = new Vector3<int>(-5, -6, -7);
        var expectedDistance = Math.Sqrt(3 * 3 + 3 * 3 + 3 * 3);

        // Act
        var actualDistance = vector1.Distance(vector2);

        // Assert
        actualDistance.Should().Be(expectedDistance);
    }
}
