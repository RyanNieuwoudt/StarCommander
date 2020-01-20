using System;
using Newtonsoft.Json;
using StarCommander.Domain.Players;

namespace StarCommander.Domain.Ships
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class ShipLocated : ShipEvent, INotifyPlayer
	{
		[JsonConstructor]
		public ShipLocated(Reference<Ship> ship, Reference<Player> player, Heading heading, Position position,
			Speed speed) : base(ship, player)
		{
			Heading = heading;
			Position = position;
			Speed = speed;
		}

		[JsonProperty]
		public Heading Heading { get; private set; }

		[JsonProperty]
		public Position Position { get; private set; }

		[JsonProperty]
		public Speed Speed { get; private set; }
	}
}