using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AmbientDbContextConfigurator;
using StarCommander.Domain;
using StarCommander.Domain.Messages;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Ships
{
	public class ShipRepository : EventsOnlyRepository<Ship, ShipDataContext>, IShipRepository
	{
		readonly ICommandRepository commandRepository;

		public ShipRepository(IAmbientDbContextConfigurator ambientDbContextConfigurator,
			ICommandRepository commandRepository, IEventPublisher eventPublisher) : base(ambientDbContextConfigurator,
			eventPublisher)
		{
			this.commandRepository = commandRepository;
		}

		public Task<ICollection<Ship>> All()
		{
			throw new NotImplementedException();
		}

		public async Task<Ship> Fetch(Reference<Ship> reference)
		{
			var ship = Ship.Launch(Reference<Ship>.None, Reference<Player>.None);

			foreach (var command in await commandRepository.FetchForTarget(reference))
			{
				switch (command.Payload)
				{
					case LaunchShip launchShip:
						ship = Ship.Launch(launchShip.Ship, launchShip.Captain);
						break;
					case SetHeading setHeading:
						ship.NavigationComputer.SetHeading(command.Created, setHeading.Heading);
						break;
					case SetSpeed setSpeed:
						ship.NavigationComputer.SetSpeed(command.Created, setSpeed.Speed);
						break;
				}
			}

			ship.ClearEvents();

			if (ship.Reference == Reference<Ship>.None)
			{
				throw new InvalidDataException($"Unable to fetch ship {reference}");
			}

			return ship;
		}

		public async Task SaveAll(ICollection<Ship> aggregates)
		{
			foreach (var aggregate in aggregates)
			{
				await Save(aggregate);
			}
		}

		public async Task RemoveAll(ICollection<Ship> aggregates)
		{
			foreach (var aggregate in aggregates)
			{
				await Remove(aggregate);
			}
		}
	}
}