using System;
using System.Threading;
using System.Threading.Tasks;
using StarCommander.Application.Projectors;
using StarCommander.Domain.Ships;

namespace StarCommander.Application.DomainEventHandlers
{
	public class ProjectShipLocations : IWhen<ShipLocated>
	{
		readonly IShipLocationProjector shipLocationProjector;

		public ProjectShipLocations(IShipLocationProjector shipLocationProjector)
		{
			this.shipLocationProjector = shipLocationProjector;
		}

		public async Task Handle(ShipLocated @event, CancellationToken cancellationToken)
		{
			await shipLocationProjector.Project(@event.Ship, @event.Position);
		}
	}
}