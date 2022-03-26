using System;
using Newtonsoft.Json;
using NodaTime;

namespace StarCommander.Domain.Messages;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class Event : Message<IDomainEvent>
{
	[JsonConstructor]
	public Event(Reference<Message<IDomainEvent>> id, Instant created, IDomainEvent payload, Instant? processed)
		: base(id, created, payload, processed)
	{
	}

	public static Event Wrap(in Reference<Message<IDomainEvent>> id, Instant now, IDomainEvent payload) =>
		new (id, now, payload, null);
}