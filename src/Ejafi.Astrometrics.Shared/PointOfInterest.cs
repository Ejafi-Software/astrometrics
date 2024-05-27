using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ejafi.Astrometrics.Shared;

[Table("pointsofinterest")]
public sealed class PointOfInterest : IEquatable<PointOfInterest>
{
    public static readonly PointOfInterest SagittariusA = new()
    {
        Name = "Sagittarius A*",
        Coordinate = new SpaceCoordinate(
            new Vector3<long>(50, 50, 50),
            Vector3<long>.Zero),
        Type = PoiType.BlackHole,
        IsPopulated = false
    };

    public static readonly PointOfInterest Earth = new()
    {
        Name = "Earth",
        Coordinate = new SpaceCoordinate(
            new Vector3<long>(-25850, 50,50),
            new Vector3<long>(46275794, 64091888, 128219890)
        ),
        IsPopulated = true,
        Type = PoiType.Earthlike
    };

    [Key]
    public Guid Id { get; init; }
    public SpaceCoordinate Coordinate { get; init; } = SpaceCoordinate.Zero;
    public required string Name { get; set; }
    public string DiscoveredBy { get; init; } = "Anonymous";
    public DateTimeOffset DiscoveredOn { get; init; } = DateTimeOffset.UtcNow;
    public string? Notes { get; set; }
    public PoiType Type { get; set; }
    public bool IsPopulated { get; set; }

    public bool IsValid()
    {
        return Name.Length >= 2;
    }

    public Sector Sector => new Sector(Coordinate);

    public bool Equals(PointOfInterest? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return GetHashCode() == other.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((PointOfInterest)obj);
    }

    public override int GetHashCode() => Coordinate.GetHashCode();

    public static bool operator ==(PointOfInterest? left, PointOfInterest? right)
    {
        return Equals(left, right);
    }

    public static bool operator !=(PointOfInterest? left, PointOfInterest? right)
    {
        return !Equals(left, right);
    }
}

public enum PoiType : byte
{
    BlackHole,
    Earthlike,
    GasGiant,
    Nebula,
    Silicate,
    Star,
    All
}
