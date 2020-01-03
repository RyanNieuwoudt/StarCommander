using System;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Messages
{
	public class Event : JsonEntity<Domain.Messages.Event, EventJson>
	{
		public DateTimeOffset Created { get; set; }
		public DateTimeOffset? Processed { get; set; }

		protected override void ProjectValues(Domain.Messages.Event @event)
		{
			Created = @event.Created;
			Processed = @event.Processed;
		}
	}
}