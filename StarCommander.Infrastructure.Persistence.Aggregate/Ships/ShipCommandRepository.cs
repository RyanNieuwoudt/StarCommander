using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StarCommander.Domain;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;
using Command = StarCommander.Domain.Messages.Command;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Ships
{
	public class ShipCommandRepository : JsonRepositoryBase<Command, Messages.Command, ShipDataContext>,
		IShipCommandRepository
	{
		public ShipCommandRepository(IAmbientDbContextConfigurator ambientDbContextConfigurator) : base(
			ambientDbContextConfigurator)
		{
		}

		public async Task<Command?> FetchNextUnprocessed()
		{
			return (await GetDbSet()
				.AsNoTracking()
				.Where(c => c.Processed == null)
				.Where(e => e.ScheduledFor == null || e.ScheduledFor <= DateTimeOffset.Now)
				.OrderBy(c => c.Created)
				.FirstOrDefaultAsync())?.ToDomain();
		}

		public async Task<Ship> Fetch(Reference<Ship> reference)
		{
			var commands = await GetDbSet()
				.AsNoTracking()
				.Where(c => c.TargetId == reference)
				.OrderBy(c => c.Created)
				.Select(c => c.ToDomain())
				.ToListAsync();

			var ship = Ship.Launch(Reference<Ship>.None, Reference<Player>.None);

			foreach (var command in commands)
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

		protected override Messages.Command AddEntity()
		{
			var entity = new Messages.Command();
			DataContext.Add(entity);
			return entity;
		}

		protected override DbSet<Messages.Command> GetDbSet()
		{
			return DataContext.ShipCommands;
		}

		protected override void RemoveEntity(Messages.Command entity)
		{
			DataContext.Remove(entity);
		}
	}
}