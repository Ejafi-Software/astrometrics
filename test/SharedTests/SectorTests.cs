using Ejafi.Astrometrics.Shared;
using FluentAssertions;

namespace SharedTests;

public class SectorTests
{
    [Theory]
    [InlineData(101,0,0, 1,0,0)]
    [InlineData(0,101,0, 0,1,0)]
    [InlineData(0,0,101, 0,0,1)]
    [InlineData(-10100,0,0, -101,0,0)]
    [InlineData(-101,0,0, -1,0,0)]
    [InlineData(0,-101,0, 0,-1,0)]
    [InlineData(0,0,-101, 0,0,-1)]
    [InlineData(1,0,0, 0,0,0)]
    [InlineData(0,-1,0, 0,0,0)]
    [InlineData(0,0,99, 0,0,1)]
    [InlineData(-99,0,0, -1,0,0)]
    [InlineData(-25800,0,0, -258,0,0)]
    public void SectorShould_Handle_Elements_Between100AndMinus100(long x, long y, long z, int expectedX, int expectedY, int expectedZ)
    {
        var coordinate = new SpaceCoordinate(new Vector3<long>(x, y, z), Vector3<long>.Zero);
        var sector = new Sector(coordinate);

        sector.X.Should().Be(expectedX);
        sector.Y.Should().Be(expectedY);
        sector.Z.Should().Be(expectedZ);
    }

    [Fact]
    public void SectorShould_Handle_Elements_EqualToZero()
    {
        var sector = new Sector(SpaceCoordinate.Zero);

        sector.X.Should().Be(0);
        sector.Y.Should().Be(0);
        sector.Z.Should().Be(0);
    }
}
