using System;
using NodaTime;
using StarCommander.Domain.Players;
using static StarCommander.Domain.Reference;

namespace StarCommander.Domain.Ships;

public class Ship : EventPublisherBase, IAggregate
{
	Ship(IClock clock, Reference<Ship> id, Reference<Player> captain)
	{
		Id = id;
		Captain = captain;
		NavigationComputer = new (clock, new ());
	}

	public NavigationComputer NavigationComputer { get; }

	public Reference<Player> Captain { get; }

	public Reference<Ship> Reference => To(this);

	public Guid Id { get; }

	public static Ship Launch(IClock clock, Reference<Ship> id, Reference<Player> captain)
	{
		var ship = new Ship(clock, id, captain);
		ship.RaiseEvent(new ShipLaunched(id, captain));
		return ship;
	}

	public void Locate()
	{
		var (instant, heading, position, speed) = NavigationComputer.Locate();
		RaiseEvent(new ShipLocated(Reference, Captain, instant, heading, position, speed));
	}
}