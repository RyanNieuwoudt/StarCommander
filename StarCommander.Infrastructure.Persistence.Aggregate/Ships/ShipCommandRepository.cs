using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StarCommander.Domain.Ships;
using StarCommander.Infrastructure.Persistence.Aggregate.Messages;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Ships
{
	public class ShipCommandRepository : JsonRepositoryBase<Domain.Messages.Command, Command, ShipDataContext>,
		IShipCommandRepository
	{
		public ShipCommandRepository(IAmbientDbContextConfigurator ambientDbContextConfigurator) : base(
			ambientDbContextConfigurator)
		{
		}

		public async Task<Domain.Messages.Command?> FetchNextUnprocessed()
		{
			return (await GetDbSet()
				.AsNoTracking()
				.Where(e => e.Processed == null)
				.OrderBy(e => e.Created)
				.FirstOrDefaultAsync())?.ToDomain();
		}

		protected override Command AddEntity()
		{
			var entity = new Command();
			DataContext.Add(entity);
			return entity;
		}

		protected override DbSet<Command> GetDbSet()
		{
			return DataContext.ShipCommands;
		}

		protected override void RemoveEntity(Command entity)
		{
			DataContext.Remove(entity);
		}
	}
}