using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Ships
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public readonly struct Time : IEquatable<Time>
	{
		[JsonProperty]
		internal int Value { get; }

		[JsonConstructor]
		public Time(int value)
		{
			Value = value;
		}

		public bool Equals(Time other)
		{
			return Value == other.Value;
		}

		public override bool Equals(object? obj)
		{
			return obj is Time other && Equals(other);
		}

		public override int GetHashCode()
		{
			return Value;
		}

		public static bool operator ==(Time left, Time right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Time left, Time right)
		{
			return !left.Equals(right);
		}

		public static Distance operator *(Time time, Speed speed)
		{
			return new Distance(time.Value * speed.Value);
		}

		public static implicit operator int(Time time)
		{
			return time.Value;
		}
	}
}