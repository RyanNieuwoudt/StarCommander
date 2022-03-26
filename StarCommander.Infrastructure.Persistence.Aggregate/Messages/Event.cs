using NodaTime;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Messages;

public class Event : JsonEntity<Domain.Messages.Event>
{
	public Instant Created { get; set; }
	public Instant? Processed { get; set; }

	protected override void ProjectValues(Domain.Messages.Event @event)
	{
		Created = @event.Created;
		Processed = @event.Processed;
	}
}