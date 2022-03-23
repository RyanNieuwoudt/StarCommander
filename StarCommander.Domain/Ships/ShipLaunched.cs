using System;
using Newtonsoft.Json;
using StarCommander.Domain.Players;

namespace StarCommander.Domain.Ships;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class ShipLaunched : ShipEvent
{
	[JsonConstructor]
	public ShipLaunched(Reference<Ship> ship, Reference<Player> player) : base(ship, player)
	{
	}
}