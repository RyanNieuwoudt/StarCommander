using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NodaTime;
using StarCommander.Application.Services;
using StarCommander.Domain.Messages;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;

namespace StarCommander.Application.DomainEventHandlers;

public class ScheduleNextLocateShip : IWhen<CaptainBoarded>, IWhen<ShipLocated>
{
	readonly IClock clock;
	readonly ICommandRepository commandRepository;
	readonly ICommandService commandService;

	public ScheduleNextLocateShip(IClock clock, ICommandRepository commandRepository, ICommandService commandService)
	{
		this.clock = clock;
		this.commandRepository = commandRepository;
		this.commandService = commandService;
	}

	public async Task Handle(CaptainBoarded @event, CancellationToken cancellationToken) =>
		await commandService.Issue(new LocateShip(@event.Ship));

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

		await commandService.Issue(new LocateShip(@event.Ship),
			clock.GetCurrentInstant().Plus(Duration.FromSeconds(3)));
	}
}