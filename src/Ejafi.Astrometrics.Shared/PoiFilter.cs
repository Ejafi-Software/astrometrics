namespace Ejafi.Astrometrics.Shared;

public record PoiFilter
{
    public static PoiFilter Default { get; } = new();
    public string Name { get; init; } = string.Empty;
    public PoiType Type { get; init; } = PoiType.All;
    public long Distance { get; init; } = long.MaxValue;
    public SpaceCoordinate CenterPoint { get; init; } = SpaceCoordinate.Zero;
    public bool IncludePopulated { get; init; }
}
