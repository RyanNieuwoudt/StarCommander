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
		readonly IShipCommandRepository shipCommandRepository;

		public ShipRepository(IAmbientDbContextConfigurator ambientDbContextConfigurator,
			IEventPublisher eventPublisher, IShipCommandRepository shipCommandRepository) : base(
			ambientDbContextConfigurator, eventPublisher)
		{
			this.shipCommandRepository = shipCommandRepository;
		}

		public Task<ICollection<Ship>> All()
		{
			throw new NotImplementedException();
		}

		public async Task<Ship> Fetch(Reference<Ship> reference)
		{
			return await shipCommandRepository.Fetch(reference);
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