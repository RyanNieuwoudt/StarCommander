using System;
using Newtonsoft.Json;
using StarCommander.Domain.Players;

namespace StarCommander.Domain.Ships;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class LaunchShip : ShipCommand
{
	[JsonConstructor]
	public LaunchShip(Reference<Ship> ship, Reference<Player> captain) : base(ship)
	{
		Captain = captain;
	}

	[JsonProperty]
	public Reference<Player> Captain { get; private set; }
}