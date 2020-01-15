using System;
using StarCommander.Domain.Players;
using static StarCommander.Domain.Reference;

namespace StarCommander.Domain.Ships
{
	public class Ship : EventPublisherBase, IAggregate
	{
		Ship(Reference<Ship> id, Reference<Player> captain)
		{
			Id = id;
			Captain = captain;
		}

		public Reference<Player> Captain { get; }

		public Reference<Ship> Reference => To(this);

		public Guid Id { get; }

		public static Ship Launch(Reference<Ship> id, Reference<Player> captain)
		{
			var ship = new Ship(id, captain);
			ship.RaiseEvent(new ShipLaunched(id, captain));
			return ship;
		}

		public void WelcomeCaptainAboard()
		{
			RaiseEvent(new CaptainBoarded(Reference, Captain));
		}
	}
}