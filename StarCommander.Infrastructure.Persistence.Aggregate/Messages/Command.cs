using System;
using NodaTime;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Messages;

public class Command : JsonEntity<Domain.Messages.Command>
{
	public Instant Created { get; set; }
	public Instant? Processed { get; set; }
	public Instant? ScheduledFor { get; set; }
	public Guid TargetId { get; set; }

	protected override void ProjectValues(Domain.Messages.Command command)
	{
		Created = command.Created;
		Processed = command.Processed;
		ScheduledFor = command.ScheduledFor;
		TargetId = command.TargetId;
	}
}