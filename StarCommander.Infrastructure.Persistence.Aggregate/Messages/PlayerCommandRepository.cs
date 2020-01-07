using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StarCommander.Domain.Players;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Messages
{
	public class PlayerCommandRepository : JsonRepositoryBase<Domain.Messages.Command, Command, MessageDataContext>,
		IPlayerCommandRepository
	{
		public PlayerCommandRepository(IAmbientDbContextConfigurator ambientDbContextConfigurator) : base(
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
			return DataContext.PlayerCommands;
		}

		protected override void RemoveEntity(Command entity)
		{
			DataContext.Remove(entity);
		}
	}
}