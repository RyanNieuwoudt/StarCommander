using System;

namespace StarCommander.Domain.Ships
{
	public readonly struct Time : IEquatable<Time>
	{
		readonly int value;

		public Time(int value)
		{
			this.value = value;
		}

		public bool Equals(Time other)
		{
			return value == other.value;
		}

		public override bool Equals(object? obj)
		{
			return obj is Time other && Equals(other);
		}

		public override int GetHashCode()
		{
			return value;
		}

		public static bool operator ==(Time left, Time right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Time left, Time right)
		{
			return !left.Equals(right);
		}
	}
}