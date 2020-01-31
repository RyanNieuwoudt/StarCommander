using System;

namespace StarCommander.Domain.Ships
{
	public readonly struct Position : IEquatable<Position>
	{
		public long X { get; }
		public long Y { get; }

		public Position(long x, long y)
		{
			X = x;
			Y = y;
		}

		public bool Equals(Position other)
		{
			return X == other.X && Y == other.Y;
		}

		public override bool Equals(object? obj)
		{
			return obj is Position other && Equals(other);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(X, Y);
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
			var dx = (long)Math.Round(Math.Sin(heading.Radians) * distance.Value);
			var dy = (long)Math.Round(GetY(heading) * distance.Value);
			return new Position(X + dx, Y + dy);
		}

		static double GetY(in Heading heading)
		{
			return heading.Value <= 180 ? -Math.Cos(heading.Radians) : Math.Cos(heading.Radians);
		}
	}
}