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
		public ShipLocated(Reference<Ship> ship, Reference<Player> player, Position position, Speed speed) : base(ship,
			player)
		{
			Position = position;
			Speed = speed;
		}

		[JsonProperty]
		public Position Position { get; private set; }

		[JsonProperty]
		public Speed Speed { get; private set; }
	}
}