using System.Threading;
using System.Threading.Tasks;
using StarCommander.Application.Services;
using StarCommander.Domain.Ships;

namespace StarCommander.Application.CommandHandlers
{
	public class ShipCommandHandler : IObey<LaunchShip>, IObey<LocateShip>, IObey<SetSpeed>
	{
		readonly IShipService shipService;

		public ShipCommandHandler(IShipService shipService)
		{
			this.shipService = shipService;
		}

		public async Task Handle(LaunchShip command, CancellationToken cancellationToken)
		{
			await shipService.Launch(command.Ship, command.Captain);
		}

		public async Task Handle(LocateShip command, CancellationToken cancellationToken)
		{
			await shipService.Locate(command.Ship);
		}

		public async Task Handle(SetSpeed command, CancellationToken cancellationToken)
		{
			await shipService.Locate(command.Ship);
		}
	}
}