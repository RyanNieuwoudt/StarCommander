using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StarCommander.Domain.Messages;
using StarCommander.Domain.Ships;

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
				.Where(e => e.Processed == null)
				.OrderBy(e => e.Created)
				.FirstOrDefaultAsync())?.ToDomain();
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