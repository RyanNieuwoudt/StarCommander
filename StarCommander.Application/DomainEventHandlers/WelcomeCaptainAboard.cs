using System.Threading;
using System.Threading.Tasks;
using StarCommander.Application.Services;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;

namespace StarCommander.Application.DomainEventHandlers
{
	public class WelcomeCaptainAboard : IWhen<PlayerSignedIn>, IWhen<ShipLaunched>
	{
		readonly IPlayerService playerService;

		public WelcomeCaptainAboard(IPlayerService playerService)
		{
			this.playerService = playerService;
		}

		public async Task Handle(PlayerSignedIn @event, CancellationToken cancellationToken)
		{
			await playerService.BoardShip(@event.Player);
		}

		public async Task Handle(ShipLaunched @event, CancellationToken cancellationToken)
		{
			await playerService.BoardShip(@event.Player);
		}
	}
}