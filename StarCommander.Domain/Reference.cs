using System;

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

	public readonly struct Reference<T> : IEquatable<Reference<T>> where T : IAggregate
	{
		public Reference(Guid id)
		{
			Id = id;
		}
		
		internal Reference(T aggregate)
		{
			Id = aggregate.Id;
		}

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

		public override bool Equals(object obj)
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