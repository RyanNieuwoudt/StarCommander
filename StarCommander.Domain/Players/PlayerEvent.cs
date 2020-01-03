using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Players
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public abstract class PlayerEvent : DomainEvent
	{
		[JsonConstructor]
		protected PlayerEvent(string callSign)
		{
			CallSign = callSign;
		}

		[JsonProperty]
		public string CallSign { get; private set; }
	}
}