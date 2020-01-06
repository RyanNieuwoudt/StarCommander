using System;

namespace StarCommander.Domain.Messages
{
	public class Job : IAggregate
	{
		public Job(Reference<Job> id, Guid queueId, Guid messageId, string handler, DateTimeOffset created)
		{
			Id = id;
			QueueId = queueId;
			MessageId = messageId;
			Handler = handler;
			Created = created;
		}

		public Guid QueueId { get; }
		public Guid MessageId { get; }
		public string Handler { get; }
		public DateTimeOffset Created { get; }

		public Guid Id { get; }
	}
}