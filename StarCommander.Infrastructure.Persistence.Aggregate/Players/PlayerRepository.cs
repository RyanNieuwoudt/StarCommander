using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StarCommander.Domain.Players;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Players
{
	public sealed class PlayerRepository : JsonRepositoryBase<Domain.Players.Player, Player, PlayerDataContext>,
		IPlayerRepository
	{
		public PlayerRepository(IAmbientDbContextConfigurator ambientDbContextConfigurator) : base(
			ambientDbContextConfigurator)
		{
		}

		public async Task<bool> Exists(string callSign)
		{
			return await GetDbSet().SingleOrDefaultAsync(u => u.CallSign == callSign) != null;
		}

		protected override Player AddEntity()
		{
			var entity = new Player();
			DataContext.Add(entity);
			return entity;
		}

		protected override DbSet<Player> GetDbSet()
		{
			return DataContext.Players;
		}

		protected override void RemoveEntity(Player entity)
		{
			DataContext.Remove(entity);
		}
	}
}