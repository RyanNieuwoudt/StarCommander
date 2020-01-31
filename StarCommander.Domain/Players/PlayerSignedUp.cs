using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Players
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class PlayerSignedUp : PlayerEvent
	{
		[JsonConstructor]
		public PlayerSignedUp(Reference<Player> player) : base(player)
		{
		}
	}
}