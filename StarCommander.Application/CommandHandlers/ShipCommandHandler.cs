using System.Threading;
using System.Threading.Tasks;
using StarCommander.Application.Services;
using StarCommander.Domain.Ships;

namespace StarCommander.Application.CommandHandlers
{
	public class ShipCommandHandler : IObey<LaunchShip>
	{
		readonly IShipService shipService;

		public ShipCommandHandler(IShipService shipService)
		{
			this.shipService = shipService;
		}

		public async Task Handle(LaunchShip command, CancellationToken cancellationToken)
		{
			await shipService.Launch(command.Captain);
		}
	}
}