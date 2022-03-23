using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Messages;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public abstract class Message<T> : IAggregate where T :  IHaveType
{
	[JsonConstructor]
	protected Message(in Reference<Message<T>> id, DateTimeOffset created, T payload, DateTimeOffset? processed)
	{
		Id = id;
		Created = created;
		Payload = payload;
		Processed = processed;
	}

	[JsonProperty]
	public DateTimeOffset Created { get; private set; }

	public bool IsProcessed => Processed.HasValue;

	[JsonProperty]
	public T Payload { get; private set; }

	[JsonProperty]
	public DateTimeOffset? Processed { get; private set; }

	[JsonProperty]
	public Guid Id { get; }

	public void MarkAsProcessed() => Processed = DateTimeOffset.Now;
}