using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Messages
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class Event : Message<IDomainEvent>
	{
		[JsonConstructor]
		public Event(Reference<Message<IDomainEvent>> id, DateTimeOffset created, IDomainEvent payload,
			DateTimeOffset? processed) : base(id, created, payload, processed)
		{
		}

		public static Event Wrap(in Reference<Message<IDomainEvent>> id, IDomainEvent payload)
		{
			return new (id, DateTimeOffset.Now, payload, null);
		}
	}
}