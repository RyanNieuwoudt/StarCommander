using System;
using Newtonsoft.Json;
using StarCommander.Domain.Players;

namespace StarCommander.Domain.Ships;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public abstract class ShipEvent : DomainEvent
{
	[JsonConstructor]
	protected ShipEvent(Reference<Ship> ship, Reference<Player> player)
	{
		Player = player;
		Ship = ship;
	}

	[JsonProperty]
	public Reference<Player> Player { get; private set; }

	[JsonProperty]
	public Reference<Ship> Ship { get; private set; }
}