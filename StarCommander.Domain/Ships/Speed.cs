using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Ships
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public readonly struct Speed : IEquatable<Speed>
	{
		public static readonly Speed Default = new Speed(default);

		[JsonProperty]
		internal long Value { get; }

		[JsonConstructor]
		public Speed(long value)
		{
			Value = value;
		}

		public bool Equals(Speed other)
		{
			return Value == other.Value;
		}

		public override bool Equals(object? obj)
		{
			return obj is Speed other && Equals(other);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
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
			return new Distance(speed.Value * time.Value);
		}

		public static implicit operator long(Speed speed)
		{
			return speed.Value;
		}
	}
}