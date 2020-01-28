using System;

namespace StarCommander.Infrastructure.Persistence.Projection.ShipLocations
{
	public class ShipLocation : ProjectWithKeyBase<ShipLocation>, IEquatable<ShipLocation>
	{
		public long ShipPositionId { get; set; }
		public Guid ShipId { get; set; }
		public double Heading { get; set; }
		public long Speed { get; set; }
		public long X { get; set; }
		public long Y { get; set; }
		public DateTimeOffset Created { get; set; }

		public bool Equals(ShipLocation? other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return ShipPositionId == other.ShipPositionId && ShipId.Equals(other.ShipId) &&
			       Heading.Equals(other.Heading) && Speed == other.Speed && X == other.X && Y == other.Y &&
			       Created.Equals(other.Created);
		}

		public override bool HasSamePrimaryKeyAs(ShipLocation other)
		{
			return ShipPositionId == other.ShipPositionId;
		}

		public override bool RepresentsTheSameDataAs(ShipLocation other)
		{
			return ShipId == other.ShipId;
		}

		protected override bool TheDataEquals(ShipLocation other)
		{
			return Heading.Equals(other.Heading) && Speed == other.Speed && X == other.X && Y == other.Y;
		}

		public override ShipLocation WithPrimaryKeyFrom(ShipLocation other)
		{
			return new ShipLocation
			{
				ShipPositionId = other.ShipPositionId,
				ShipId = ShipId,
				Heading = Heading,
				Speed = Speed,
				X = X,
				Y = Y,
				Created = Created
			};
		}

		public override bool Equals(object? obj)
		{
			if (ReferenceEquals(null, obj))
			{
				return false;
			}

			if (ReferenceEquals(this, obj))
			{
				return true;
			}

			return obj.GetType() == GetType() && Equals((ShipLocation)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(ShipPositionId, ShipId, Heading, Speed, X, Y, Created);
		}

		public static bool operator ==(ShipLocation? left, ShipLocation? right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(ShipLocation? left, ShipLocation? right)
		{
			return !Equals(left, right);
		}
	}
}