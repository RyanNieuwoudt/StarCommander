using System;

namespace StarCommander.Infrastructure.Persistence.Projection.ShipPositions
{
	public class ShipPosition : ProjectWithKeyBase<ShipPosition>, IEquatable<ShipPosition>
	{
		public long ShipPositionId { get; set; }
		public Guid ShipId { get; set; }
		public long X { get; set; }
		public long Y { get; set; }
		public DateTimeOffset Created { get; set; }

		public bool Equals(ShipPosition? other)
		{
			if (ReferenceEquals(null, other))
			{
				return false;
			}

			if (ReferenceEquals(this, other))
			{
				return true;
			}

			return ShipPositionId == other.ShipPositionId && ShipId.Equals(other.ShipId) && X == other.X &&
			       Y == other.Y && Created.Equals(other.Created);
		}

		public override bool HasSamePrimaryKeyAs(ShipPosition other)
		{
			return ShipPositionId == other.ShipPositionId;
		}

		public override bool RepresentsTheSameDataAs(ShipPosition other)
		{
			return ShipId == other.ShipId;
		}

		protected override bool TheDataEquals(ShipPosition other)
		{
			return X == other.X && Y == other.Y;
		}

		public override ShipPosition WithPrimaryKeyFrom(ShipPosition other)
		{
			return new ShipPosition
			{
				ShipPositionId = other.ShipPositionId,
				ShipId = ShipId,
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

			return obj.GetType() == GetType() && Equals((ShipPosition)obj);
		}

		public override int GetHashCode()
		{
			return HashCode.Combine(ShipPositionId, ShipId, X, Y, Created);
		}

		public static bool operator ==(ShipPosition? left, ShipPosition? right)
		{
			return Equals(left, right);
		}

		public static bool operator !=(ShipPosition? left, ShipPosition? right)
		{
			return !Equals(left, right);
		}
	}
}