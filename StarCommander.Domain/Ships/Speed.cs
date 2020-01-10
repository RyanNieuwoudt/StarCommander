using System;

namespace StarCommander.Domain.Ships
{
	public readonly struct Speed : IEquatable<Speed>
	{
		readonly long value;

		public Speed(long value)
		{
			this.value = value;
		}

		public bool Equals(Speed other)
		{
			return value == other.value;
		}

		public override bool Equals(object? obj)
		{
			return obj is Speed other && Equals(other);
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public static bool operator ==(Speed left, Speed right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Speed left, Speed right)
		{
			return !left.Equals(right);
		}
	}
}