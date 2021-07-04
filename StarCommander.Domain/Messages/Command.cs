using System;
using Newtonsoft.Json;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;

namespace StarCommander.Domain.Messages
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class Command : Message<ICommand>
	{
		[JsonConstructor]
		public Command(Reference<Message<ICommand>> id, Guid targetId, DateTimeOffset created, ICommand payload,
			DateTimeOffset? processed, DateTimeOffset? scheduledFor) : base(id, created, payload, processed)
		{
			TargetId = targetId;
			ScheduledFor = scheduledFor;
		}

		[JsonProperty]
		public DateTimeOffset? ScheduledFor { get; private set; }

		[JsonProperty]
		public Guid TargetId { get; private set; }

		public static Command Wrap(in Reference<Message<ICommand>> id, ICommand payload, DateTimeOffset? scheduledFor)
		{
			return new (id, ExtractTargetId(payload), DateTimeOffset.Now, payload, null, scheduledFor);
		}

		static Guid ExtractTargetId(ICommand command)
		{
			return command switch
			{
				PlayerCommand playerCommand => playerCommand.Player,
				ShipCommand shipCommand => shipCommand.Ship,
				_ => throw new InvalidOperationException()
			};
		}
	}
}