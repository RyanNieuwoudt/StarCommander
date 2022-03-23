using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Players;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public abstract class PlayerCommand : Command
{
	[JsonConstructor]
	protected PlayerCommand(Reference<Player> player, string callSign)
	{
		Player = player;
		CallSign = callSign;
	}

	[JsonProperty]
	public string CallSign { get; private set; }

	[JsonProperty]
	public Reference<Player> Player { get; private set; }
}