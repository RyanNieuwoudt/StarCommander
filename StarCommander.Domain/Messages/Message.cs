using System;
using Newtonsoft.Json;
using NodaTime;

namespace StarCommander.Domain.Messages;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public abstract class Message<T> : IAggregate where T :  IHaveType
{
	[JsonConstructor]
	protected Message(in Reference<Message<T>> id, Instant created, T payload, Instant? processed)
	{
		Id = id;
		Created = created;
		Payload = payload;
		Processed = processed;
	}

	[JsonProperty]
	public Instant Created { get; private set; }

	public bool IsProcessed => Processed.HasValue;

	[JsonProperty]
	public T Payload { get; private set; }

	[JsonProperty]
	public Instant? Processed { get; private set; }

	[JsonProperty]
	public Guid Id { get; }

	public void MarkAsProcessed(Instant now) => Processed = now;
}