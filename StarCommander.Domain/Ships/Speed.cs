using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Ships;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public readonly struct Speed : IEquatable<Speed>
{
	public static readonly Speed Default = new (default);

	[JsonProperty]
	readonly long value;

	[JsonConstructor]
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

	public static Distance operator *(Speed speed, Time time)
	{
		return new (speed.value * time);
	}

	public static implicit operator long(Speed speed)
	{
		return speed.value;
	}
}