using System;
using Newtonsoft.Json;
using NodaTime;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;

namespace StarCommander.Domain.Messages;

[Serializable]
[JsonObject(MemberSerialization.OptIn)]
public class Command : Message<ICommand>
{
	[JsonConstructor]
	public Command(Reference<Message<ICommand>> id, Guid targetId, Instant created, ICommand payload,
		Instant? processed, Instant? scheduledFor) : base(id, created, payload, processed)
	{
		TargetId = targetId;
		ScheduledFor = scheduledFor;
	}

	[JsonProperty]
	public Instant? ScheduledFor { get; private set; }

	[JsonProperty]
	public Guid TargetId { get; private set; }

	public static Command
		Wrap(in Reference<Message<ICommand>> id, ICommand payload, Instant now, Instant? scheduledFor) =>
		new (id, ExtractTargetId(payload), now, payload, null, scheduledFor);

	static Guid ExtractTargetId(ICommand command) => command switch
	{
		PlayerCommand playerCommand => playerCommand.Player,
		ShipCommand shipCommand => shipCommand.Ship,
		_ => throw new InvalidOperationException()
	};
}