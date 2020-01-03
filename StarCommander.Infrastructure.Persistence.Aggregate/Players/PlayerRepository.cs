using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StarCommander.Domain.Messages;
using StarCommander.Domain.Players;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Players
{
	public class PlayerRepository :
		EventPublishingRepositoryBase<Domain.Players.Player, PlayerJson, Player, PlayerDataContext>, IPlayerRepository
	{
		public PlayerRepository(IAmbientDbContextConfigurator ambientDbContextConfigurator,
			IEventPublisher eventPublisher) : base(ambientDbContextConfigurator, eventPublisher)
		{
		}

		public async Task<bool> Exists(string callSign)
		{
			return await GetDbSet().SingleOrDefaultAsync(u => u.CallSign == callSign) != null;
		}

		public async Task<Domain.Players.Player> Fetch(string callSign)
		{
			return (await GetDbSet().SingleAsync(u => u.CallSign == callSign)).ToDomain();
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