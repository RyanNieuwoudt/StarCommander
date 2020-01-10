using System;

namespace StarCommander.Domain.Ships
{
	public readonly struct Heading : IEquatable<Heading>
	{
		readonly double value;

		public Heading(double value)
		{
			this.value = value;
		}

		public bool Equals(Heading other)
		{
			return value.Equals(other.value);
		}

		public override bool Equals(object? obj)
		{
			return obj is Heading other && Equals(other);
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public static bool operator ==(Heading left, Heading right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Heading left, Heading right)
		{
			return !left.Equals(right);
		}
	}
}