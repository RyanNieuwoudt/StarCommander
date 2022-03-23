using System.Threading;
using System.Threading.Tasks;
using StarCommander.Application.Services;
using StarCommander.Domain.Ships;

namespace StarCommander.Application.DomainEventHandlers;

public class AssignShip : IWhen<ShipLaunched>
{
	readonly IPlayerService playerService;

	public AssignShip(IPlayerService playerService)
	{
		this.playerService = playerService;
	}

	public async Task Handle(ShipLaunched @event, CancellationToken cancellationToken)
	{
		await playerService.AssignShip(@event.Player, @event.Ship);
	}
}