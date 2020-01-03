using System;

namespace StarCommander.Domain.Messages
{
	public class Event : Message<IDomainEvent>
	{
		protected Event()
		{
		}

		public Event(Reference<Message<IDomainEvent>> id, DateTimeOffset created, IDomainEvent payload,
			DateTimeOffset? processed) : base(id, created, payload, processed)
		{
		}

		public static Event Wrap(in Reference<Message<IDomainEvent>> id, IDomainEvent payload)
		{
			return new Event(id, DateTimeOffset.Now, payload, null);
		}
	}
}