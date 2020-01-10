using System;

namespace StarCommander.Domain.Messages
{
	public class Job : IAggregate
	{
		public const string DomainEvents = "DomainEvents";
		public const string PlayerCommands = "PlayerCommands";
		public const string ShipCommands = "ShipCommands";

		public Job(Reference<Job> id, Guid queueId, Guid messageId, string address, string handler,
			DateTimeOffset created)
		{
			Id = id;
			QueueId = queueId;
			MessageId = messageId;
			Address = address;
			Handler = handler;
			Created = created;
		}

		public Guid QueueId { get; }
		public Guid MessageId { get; }
		public string Address { get; }
		public string Handler { get; }
		public DateTimeOffset Created { get; }

		public Guid Id { get; }
	}
}