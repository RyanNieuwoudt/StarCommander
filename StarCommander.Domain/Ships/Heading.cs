using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Ships
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public readonly struct Heading : IEquatable<Heading>
	{
		[JsonProperty]
		internal double Value { get; }

		internal double Radians { get; }

		[JsonConstructor]
		public Heading(double value)
		{
			Value = value;
			Radians = Math.PI * Value / 180.0;
		}

		public bool Equals(Heading other)
		{
			return Value.Equals(other.Value);
		}

		public override bool Equals(object? obj)
		{
			return obj is Heading other && Equals(other);
		}

		public override int GetHashCode()
		{
			return Value.GetHashCode();
		}

		public static bool operator ==(Heading left, Heading right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(Heading left, Heading right)
		{
			return !left.Equals(right);
		}

		public static implicit operator double(Heading heading)
		{
			return heading.Value;
		}
	}
}