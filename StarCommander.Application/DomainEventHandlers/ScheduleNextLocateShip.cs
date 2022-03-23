using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using StarCommander.Application.Services;
using StarCommander.Domain.Messages;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;

namespace StarCommander.Application.DomainEventHandlers;

public class ScheduleNextLocateShip : IWhen<CaptainBoarded>, IWhen<ShipLocated>
{
	readonly ICommandRepository commandRepository;
	readonly ICommandService commandService;

	public ScheduleNextLocateShip(ICommandRepository commandRepository, ICommandService commandService)
	{
		this.commandRepository = commandRepository;
		this.commandService = commandService;
	}

	public async Task Handle(CaptainBoarded @event, CancellationToken cancellationToken)
	{
		await commandService.Issue(new LocateShip(@event.Ship));
	}

	public async Task Handle(ShipLocated @event, CancellationToken cancellationToken)
	{
		if (@event.Speed == 0)
		{
			return;
		}

		var scheduledCommands = await commandRepository.FetchScheduledForTarget(@event.Ship);
		if (scheduledCommands.Any(command => command.Payload is LocateShip))
		{
			return;
		}

		await commandService.Issue(new LocateShip(@event.Ship), DateTimeOffset.Now.AddSeconds(3));
	}
}