namespace Ejafi.Astrometrics.Shared;

public struct Sector
{
    public long X { get; private set; }
    public long Y { get; private set; }
    public long Z { get; private set; }

    public Sector(){}

    public Sector(SpaceCoordinate coordinate)
    {
        CalculateSector(coordinate);
    }

    public Sector(long x, long y, long z)
    {
        X = x;
        Y = y;
        Z = z;
    }

    public override string ToString()
    {
        return $"{X}, {Y}, {Z}";
    }

    private void CalculateSector(SpaceCoordinate coordinate)
    {
        X = CalculateVectorElement(coordinate.Galactic.X);
        Y = CalculateVectorElement(coordinate.Galactic.Y);
        Z = CalculateVectorElement(coordinate.Galactic.Z);
    }

    private static int CalculateVectorElement(long element)
    {
        return element switch
        {
            0 => 0,
            >= 100 or <= -100 => (int)Math.Round(element / (double)100),
            < 0 => (int)Math.Ceiling(element / (double)50),
            _ => (int)Math.Floor(element / (double)50)
        };
    }
}
