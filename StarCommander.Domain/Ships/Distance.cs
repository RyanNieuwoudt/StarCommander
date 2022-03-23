using System;

namespace StarCommander.Domain.Ships;

public readonly struct Distance : IEquatable<Distance>
{
	internal long Value { get; }

	public Distance(long value)
	{
		Value = value;
	}

	public bool Equals(Distance other)
	{
		return Value == other.Value;
	}

	public override bool Equals(object? obj)
	{
		return obj is Distance other && Equals(other);
	}

	public override int GetHashCode()
	{
		return Value.GetHashCode();
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