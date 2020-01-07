using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Messages
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class Command : Message<ICommand>
	{
		[JsonConstructor]
		public Command(Reference<Message<ICommand>> id, Guid targetId, DateTimeOffset created, ICommand payload,
			DateTimeOffset? processed) : base(id, created, payload, processed)
		{
			TargetId = targetId;
		}

		[JsonProperty]
		public Guid TargetId { get; private set; }

		public static Command Wrap(in Reference<Message<ICommand>> id, Guid targetId, ICommand payload)
		{
			return new Command(id, targetId, DateTimeOffset.Now, payload, null);
		}
	}
}