using System;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Messages
{
	public class Command : JsonEntity<Domain.Messages.Command>
	{
		public DateTimeOffset Created { get; set; }
		public DateTimeOffset? Processed { get; set; }

		protected override void ProjectValues(Domain.Messages.Command command)
		{
			Created = command.Created;
			Processed = command.Processed;
		}
	}
}