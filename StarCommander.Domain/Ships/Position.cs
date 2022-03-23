using System;

namespace StarCommander.Domain.Ships;

public readonly struct Position : IEquatable<Position>
{
	public long X { get; }
	public long Y { get; }

	public Position(long x, long y)
	{
		X = x;
		Y = y;
	}

	public bool Equals(Position other) => X == other.X && Y == other.Y;

	public override bool Equals(object? obj) => obj is Position other && Equals(other);

	public override int GetHashCode() => HashCode.Combine(X, Y);

	public static bool operator ==(Position left, Position right) => left.Equals(right);

	public static bool operator !=(Position left, Position right) => !left.Equals(right);

	public Position Apply(Heading heading, Distance distance) => new (
		X + (long)Math.Round(Math.Sin(heading.Radians) * distance.Value),
		Y + (long)Math.Round(GetY(heading) * distance.Value));

	static double GetY(in Heading heading) =>
		heading.Value <= 180 ? -Math.Cos(heading.Radians) : Math.Cos(heading.Radians);
}