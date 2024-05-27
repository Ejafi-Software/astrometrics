using System.Numerics;

namespace Ejafi.Astrometrics.Shared
{
    /// <summary>
    /// Represents a three-dimensional vector with generic numeric components.
    /// </summary>
    /// <typeparam name="T">The numeric type of the vector's components.</typeparam>
    public sealed class Vector3<T> : IEquatable<Vector3<T>> where T : struct, INumber<T>
    {
        /// <summary>
        /// Represents a vector with all components set to their default values.
        /// </summary>
        public static Vector3<T> Zero => new(default, default, default);

        /// <summary>
        /// Gets or sets the X component of the vector.
        /// </summary>
        public T X { get; set; }

        /// <summary>
        /// Gets or sets the Y component of the vector.
        /// </summary>
        public T Y { get; set; }

        /// <summary>
        /// Gets or sets the Z component of the vector.
        /// </summary>
        public T Z { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3{T}"/> class.
        /// </summary>
        public Vector3() { }

        /// <summary>
        /// Initializes a new instance of the <see cref="Vector3{T}"/> class with the specified components.
        /// </summary>
        /// <param name="x">The X component.</param>
        /// <param name="y">The Y component.</param>
        /// <param name="z">The Z component.</param>
        public Vector3(T x, T y, T z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Calculates the distance between this vector and another vector.
        /// </summary>
        /// <param name="other">The other vector.</param>
        /// <returns>The distance between the two vectors.</returns>
        public double Distance(Vector3<T> other) => Distance(this, other);

        /// <summary>
        /// Calculates the distance between two vectors.
        /// </summary>
        /// <param name="v1">The first vector.</param>
        /// <param name="v2">The second vector.</param>
        /// <returns>The distance between the two vectors.</returns>
        public static double Distance(Vector3<T> v1, Vector3<T> v2)
        {
            var deltaX = v1.X - v2.X;
            var deltaY = v1.Y - v2.Y;
            var deltaZ = v1.Z - v2.Z;

            // Compute the distance using the Euclidean formula
            return Math.Sqrt(
                Convert.ToDouble(deltaX) * Convert.ToDouble(deltaX) +
                Convert.ToDouble(deltaY) * Convert.ToDouble(deltaY) +
                Convert.ToDouble(deltaZ) * Convert.ToDouble(deltaZ)
            );
        }

        /// <summary>
        /// Determines whether this vector is equal to another vector.
        /// </summary>
        /// <param name="other">The other vector.</param>
        /// <returns>True if the vectors are equal; otherwise, false.</returns>
        public bool Equals(Vector3<T>? other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return X == other.X && Y == other.Y && Z == other.Z;
        }

        /// <summary>
        /// Determines whether this vector is equal to a specified object.
        /// </summary>
        /// <param name="obj">The object to compare with.</param>
        /// <returns>True if the vectors are equal; otherwise, false.</returns>
        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((Vector3<T>)obj);
        }

        /// <summary>
        /// Serves as the default hash function.
        /// </summary>
        /// <returns>A hash code for the current object.</returns>
        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y, Z);
        }

        /// <summary>
        /// Determines whether two vectors are equal.
        /// </summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>True if the vectors are equal; otherwise, false.</returns>
        public static bool operator ==(Vector3<T>? left, Vector3<T>? right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Determines whether two vectors are not equal.
        /// </summary>
        /// <param name="left">The first vector.</param>
        /// <param name="right">The second vector.</param>
        /// <returns>True if the vectors are not equal; otherwise, false.</returns>
        public static bool operator !=(Vector3<T>? left, Vector3<T>? right)
        {
            return !Equals(left, right);
        }

        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{X}, {Y}, {Z}";
        }
    }
}
