using System;

namespace StarCommander.Infrastructure.Persistence.Projection.ShipLocations;

public class ShipLocation : ProjectWithKeyBase<ShipLocation>, IEquatable<ShipLocation>
{
	public long ShipLocationId { get; private init; }
	public Guid ShipId { get; init; }
	public double Heading { get; init; }
	public long Speed { get; init; }
	public long X { get; init; }
	public long Y { get; init; }
	public DateTimeOffset Created { get; init; }

	public bool Equals(ShipLocation? other) => !ReferenceEquals(null, other) && (ReferenceEquals(this, other) ||
		ShipLocationId == other.ShipLocationId && ShipId.Equals(other.ShipId) && Heading.Equals(other.Heading) &&
		Speed == other.Speed && X == other.X && Y == other.Y && Created.Equals(other.Created));

	public override bool HasSamePrimaryKeyAs(ShipLocation other) => ShipLocationId == other.ShipLocationId;

	public override bool RepresentsTheSameDataAs(ShipLocation other) => ShipId == other.ShipId;

	protected override bool TheDataEquals(ShipLocation other) =>
		Heading.Equals(other.Heading) && Speed == other.Speed && X == other.X && Y == other.Y;

	public override ShipLocation WithPrimaryKeyFrom(ShipLocation other) => new ()
	{
		ShipLocationId = other.ShipLocationId,
		ShipId = ShipId,
		Heading = Heading,
		Speed = Speed,
		X = X,
		Y = Y,
		Created = Created
	};

	public override bool Equals(object? obj) =>
		!ReferenceEquals(null, obj) &&
		(ReferenceEquals(this, obj) || obj.GetType() == GetType() && Equals((ShipLocation)obj));

	public override int GetHashCode() => HashCode.Combine(ShipLocationId, ShipId, Heading, Speed, X, Y, Created);

	public static bool operator ==(ShipLocation? left, ShipLocation? right) => Equals(left, right);

	public static bool operator !=(ShipLocation? left, ShipLocation? right) => !Equals(left, right);
}