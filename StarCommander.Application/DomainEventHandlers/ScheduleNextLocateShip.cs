using System;
using System.Threading;
using System.Threading.Tasks;
using StarCommander.Application.Services;
using StarCommander.Domain.Ships;

namespace StarCommander.Application.DomainEventHandlers
{
	public class ScheduleNextLocateShip : IWhen<ShipLocated>
	{
		readonly ICommandService commandService;

		public ScheduleNextLocateShip(ICommandService commandService)
		{
			this.commandService = commandService;
		}

		public async Task Handle(ShipLocated @event, CancellationToken cancellationToken)
		{
			if (@event.Speed != 0)
			{
				await commandService.Issue(new LocateShip(@event.Ship), DateTimeOffset.Now.AddSeconds(3));
			}
		}
	}
}