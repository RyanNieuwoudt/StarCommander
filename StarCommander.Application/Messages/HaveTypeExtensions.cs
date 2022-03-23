using System;
using StarCommander.Domain;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;
using static StarCommander.Domain.Messages.Job;

namespace StarCommander.Application.Messages;

static class HaveTypeExtensions
{
	internal static string GetAddress(this IHaveType payload)
	{
		return payload switch
		{
			ICommand c => Commands,
			IDomainEvent e => DomainEvents,
			_ => throw new InvalidOperationException()
		};
	}

	internal static Guid GetQueueId(this IHaveType payload)
	{
		return payload switch
		{
			PlayerCommand playerCommand => playerCommand.Player,
			PlayerEvent playerEvent => playerEvent.Player,
			ShipCommand shipCommand => shipCommand.Ship,
			ShipEvent shipEvent => shipEvent.Ship,
			_ => Guid.Empty
		};
	}
}