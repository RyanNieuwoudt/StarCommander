using System.Threading;
using System.Threading.Tasks;
using StarCommander.Application.Services;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;

namespace StarCommander.Application.DomainEventHandlers
{
	public class LaunchShip : IWhen<PlayerSignedUp>
	{
		readonly ICommandService commandService;
		readonly IReferenceGenerator generator;

		public LaunchShip(ICommandService commandService, IReferenceGenerator generator)
		{
			this.commandService = commandService;
			this.generator = generator;
		}

		public async Task Handle(PlayerSignedUp @event, CancellationToken cancellationToken)
		{
			await commandService.Issue(new Domain.Ships.LaunchShip(generator.NewReference<Ship>(), @event.Player));
		}
	}
}