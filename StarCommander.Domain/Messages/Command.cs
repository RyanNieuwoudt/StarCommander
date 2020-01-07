using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Messages
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class Command<T> : Message<ICommand> where T : notnull, IAggregate
	{
		[JsonConstructor]
		public Command(Reference<Message<ICommand>> id, Reference<T> targetId, DateTimeOffset created, ICommand payload,
			DateTimeOffset? processed) : base(id, created, payload, processed)
		{
			TargetId = targetId;
		}

		[JsonProperty]
		public Reference<T> TargetId { get; private set; }

		public static Command<T> Wrap(in Reference<Message<ICommand>> id, Reference<T> targetId, ICommand payload)
		{
			return new Command<T>(id, targetId, DateTimeOffset.Now, payload, null);
		}
	}
}