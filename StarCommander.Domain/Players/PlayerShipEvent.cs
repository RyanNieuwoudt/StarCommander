using System;
using Newtonsoft.Json;
using StarCommander.Domain.Ships;

namespace StarCommander.Domain.Players
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public abstract class PlayerShipEvent : PlayerEvent
	{
		[JsonConstructor]
		protected PlayerShipEvent(Reference<Player> player, Reference<Ship> ship) : base(player)
		{
			Ship = ship;
		}

		[JsonProperty]
		public Reference<Ship> Ship { get; private set; }
	}
}