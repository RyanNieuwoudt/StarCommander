using System;
using NodaTime;

namespace StarCommander.Domain.Messages;

public class Job : IAggregate
{
	public const string Commands = "Commands";
	public const string DomainEvents = "DomainEvents";

	public Job(Reference<Job> id, string address, string handler, Guid messageId, Guid queueId, Instant created)
	{
		Id = id;

		Address = address;
		Handler = handler;
		MessageId = messageId;
		QueueId = queueId;

		Created = created;
	}

	public string Address { get; }
	public string Handler { get; }
	public Guid MessageId { get; }
	public Guid QueueId { get; }

	public Instant Created { get; }

	public Guid Id { get; }
}