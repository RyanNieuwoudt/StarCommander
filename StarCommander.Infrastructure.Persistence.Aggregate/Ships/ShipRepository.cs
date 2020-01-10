using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using StarCommander.Domain;
using StarCommander.Domain.Messages;
using StarCommander.Domain.Ships;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Ships
{
	public class ShipRepository : EventsOnlyRepository<Ship, ShipDataContext>, IShipRepository
	{
		public ShipRepository(IAmbientDbContextConfigurator ambientDbContextConfigurator,
			IEventPublisher eventPublisher) : base(ambientDbContextConfigurator, eventPublisher)
		{
		}

		public Task<ICollection<Ship>> All()
		{
			throw new NotImplementedException();
		}

		public Task<Ship> Fetch(Reference<Ship> reference)
		{
			throw new NotImplementedException();
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