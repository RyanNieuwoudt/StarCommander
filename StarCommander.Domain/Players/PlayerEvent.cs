using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Players
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public abstract class PlayerEvent : DomainEvent
	{
		[JsonConstructor]
		protected PlayerEvent(Reference<Player> player)
		{
			Player = player;
		}

		[JsonProperty]
		public Reference<Player> Player { get; private set; }
	}
}