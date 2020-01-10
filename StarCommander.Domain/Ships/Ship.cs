using System;
using StarCommander.Domain.Players;

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

		public Guid Id { get; }

		public static Ship Launch(Reference<Ship> id, Reference<Player> captain)
		{
			return new Ship(id, captain);
		}
	}
}