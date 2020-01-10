using System;

namespace StarCommander.Domain.Ships
{
	public readonly struct Speed : IEquatable<Speed>
	{
		internal long Value { get; }

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
	}
}