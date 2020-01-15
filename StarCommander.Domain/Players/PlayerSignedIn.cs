using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Players
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class PlayerSignedIn : PlayerEvent
	{
		[JsonConstructor]
		public PlayerSignedIn(Reference<Player> player) : base(player)
		{
		}
	}
}