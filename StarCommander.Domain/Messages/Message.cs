using System;

namespace StarCommander.Domain.Messages
{
	public abstract class Message<T> : IAggregate
	{
		protected Message()
		{
		}

		protected Message(in Reference<Message<T>> id, DateTimeOffset created, T payload, DateTimeOffset? processed)
		{
			Id = id;
			Created = created;
			Payload = payload;
			Processed = processed;
		}

		public DateTimeOffset Created { get; protected set; }

		public bool IsProcessed => Processed.HasValue;

		public T Payload { get; protected set; }

		public DateTimeOffset? Processed { get; protected set; }

		public Guid Id { get; protected set; }

		public void MarkAsProcessed()
		{
			Processed = DateTimeOffset.Now;
		}
	}
}