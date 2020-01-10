using System;

namespace StarCommander.Domain.Ships
{
	public readonly struct Distance : IEquatable<Distance>
	{
		readonly long value;

		public Distance(long value)
		{
			this.value = value;
		}

		public bool Equals(Distance other)
		{
			return value == other.value;
		}

		public override bool Equals(object? obj)
		{
			return obj is Distance other && Equals(other);
		}

		public override int GetHashCode()
		{
			return value.GetHashCode();
		}

		public static bool operator ==(Distance left, Distance right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Distance left, Distance right)
		{
			return !left.Equals(right);
		}
	}
}