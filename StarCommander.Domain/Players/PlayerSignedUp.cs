using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Players
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class PlayerSignedUp : PlayerEvent, INotifyPlayer
	{
		[JsonConstructor]
		public PlayerSignedUp(Reference<Player> player, string callSign) : base(player, callSign)
		{
		}
	}
}