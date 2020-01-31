using System;
using Newtonsoft.Json;

namespace StarCommander.Domain
{
	public static class Reference
	{
		public static Reference<T> To<T>(Guid id) where T : IAggregate
		{
			return new Reference<T>(id);
		}
		
		internal static Reference<T> To<T>(T aggregate) where T : IAggregate
		{
			return new Reference<T>(aggregate);
		}
	}

	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public readonly struct Reference<T> : IEquatable<Reference<T>> where T : IAggregate
	{
		public static Reference<T> None = new Reference<T>(Guid.Empty);
		
		[JsonConstructor]
		public Reference(Guid id)
		{
			Id = id;
		}
		
		internal Reference(T aggregate)
		{
			Id = aggregate.Id;
		}

		[JsonProperty]
		public Guid Id { get; }

		public bool Equals(Reference<T> other)
		{
			return Id == other.Id;
		}

		public static bool operator ==(Reference<T> left, Reference<T> right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(Reference<T> left, Reference<T> right)
		{
			return !Equals(left, right);
		}

		public override bool Equals(object? obj)
		{
			if (obj is null)
			{
				return false;
			}

			return obj is Reference<T> && Equals((Reference<T>)obj);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}
		
		public static implicit operator Guid(Reference<T> reference)
		{
			return reference.Id;
		}

		public override string ToString()
		{
			return Id.ToString();
		}
	}
}