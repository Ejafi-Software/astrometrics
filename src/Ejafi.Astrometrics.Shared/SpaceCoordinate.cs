namespace Ejafi.Astrometrics.Shared
{
    /// <summary>
    /// Represents a space coordinate with galactic and local vectors.
    /// </summary>
    public sealed class SpaceCoordinate : IEquatable<SpaceCoordinate>
    {
        private const double LightYearToKilometers = 9.461e12;

        /// <summary>
        /// A static readonly instance representing the zero coordinate.
        /// </summary>
        public static SpaceCoordinate Zero => new();

        /// <summary>
        /// Gets or sets the galactic coordinates.
        /// </summary>
        public Vector3<long> Galactic { get; set; } = Vector3<long>.Zero;

        /// <summary>
        /// Gets or sets the local coordinates.
        /// </summary>
        public Vector3<long> Local { get; set; } = Vector3<long>.Zero;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpaceCoordinate"/> class.
        /// </summary>
        private SpaceCoordinate() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpaceCoordinate"/> class with specified galactic and local vectors.
        /// </summary>
        /// <param name="galactic">The galactic vector.</param>
        /// <param name="local">The local vector.</param>
        public SpaceCoordinate(Vector3<long> galactic, Vector3<long> local)
        {
            Galactic = galactic;
            Local = local;
        }

        /// <summary>
        /// Calculates the distance between this space coordinate and another.
        /// </summary>
        /// <param name="other">The other space coordinate.</param>
        /// <returns>A <see cref="Distance"/> object representing the distance and its units.</returns>
        public Distance CalculateDistance(SpaceCoordinate other)
        {
            // Convert this coordinate to absolute values in kilometers
            var thisAbsolute = new Vector3<double>(
                Local.X + Galactic.X * LightYearToKilometers,
                Local.Y + Galactic.Y * LightYearToKilometers,
                Local.Z + Galactic.Z * LightYearToKilometers
            );

            // Convert the other coordinate to absolute values in kilometers
            var otherAbsolute = new Vector3<double>(
                other.Local.X + other.Galactic.X * LightYearToKilometers,
                other.Local.Y + other.Galactic.Y * LightYearToKilometers,
                other.Local.Z + other.Galactic.Z * LightYearToKilometers
            );

            // Calculate the distance between the two coordinates
            var distance = Vector3<double>.Distance(thisAbsolute, otherAbsolute);
            var scaled = distance / LightYearToKilometers;

            // Determine the units for the distance
            return new Distance(
                scaled >= 0.25 ? scaled : distance,
                scaled >= 0.25 ? "LY" : "KM"
            );
        }

        /// <summary>
        /// Represents a distance with magnitude and units.
        /// </summary>
        /// <param name="Magnitude">The magnitude of the distance.</param>
        /// <param name="Units">The units of the distance.</param>
        public record Distance(double Magnitude, string Units);

        public bool Equals(SpaceCoordinate? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Galactic.Equals(other.Galactic) && Local.Equals(other.Local);
        }

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((SpaceCoordinate)obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Galactic, Local);
        }

        public static bool operator ==(SpaceCoordinate? left, SpaceCoordinate? right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(SpaceCoordinate? left, SpaceCoordinate? right)
        {
            return !Equals(left, right);
        }
    }
}
