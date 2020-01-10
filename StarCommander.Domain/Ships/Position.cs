using System;

namespace StarCommander.Domain.Ships
{
	public readonly struct Position : IEquatable<Position>
	{
		readonly long x;
		readonly long y;

		public Position(long x, long y)
		{
			this.x = x;
			this.y = y;
		}

		public bool Equals(Position other)
		{
			return x == other.x && y == other.y;
		}

		public override bool Equals(object? obj)
		{
			return obj is Position other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(x, y);
		}

		public static bool operator ==(Position left, Position right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Position left, Position right)
		{
			return !left.Equals(right);
		}

		public Position Apply(Heading heading, Distance distance)
		{
			return this;
		}
	}
}