using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Ships
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public readonly struct Time : IEquatable<Time>
	{
		[JsonProperty]
		readonly long value;

		[JsonConstructor]
		public Time(long value)
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
			return value.GetHashCode();
		}

		public static bool operator ==(Time left, Time right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Time left, Time right)
		{
			return !left.Equals(right);
		}

		public static implicit operator long(Time time)
		{
			return time.value;
		}

		public static Distance operator *(Time time, Speed speed)
		{
			return new Distance(time.value * speed);
		}
	}
}