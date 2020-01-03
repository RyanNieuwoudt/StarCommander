using System;
using StarCommander.Domain;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Messages
{
	public class EventJson : Domain.Messages.Event
	{
		public new Guid Id
		{
			get => base.Id;
			set => base.Id = value;
		}

		public new DateTimeOffset Created
		{
			get => base.Created;
			set => base.Created = value;
		}

		public new IDomainEvent Payload
		{
			get => base.Payload;
			set => base.Payload = value;
		}

		public DateTimeOffset? Processed
		{
			get => base.Processed;
			set => base.Processed = value;
		}
	}
}