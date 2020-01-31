using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StarCommander.Domain.Messages;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Messages
{
	public class EventRepository : JsonRepositoryBase<Domain.Messages.Event, Event, MessageDataContext>,
		IEventRepository
	{
		public EventRepository(IAmbientDbContextConfigurator ambientDbContextConfigurator) : base(
			ambientDbContextConfigurator)
		{
		}

		public async Task<Domain.Messages.Event?> FetchNextUnprocessed()
		{
			return (await GetDbSet()
				.AsNoTracking()
				.Where(e => e.Processed == null)
				.OrderBy(e => e.Created)
				.FirstOrDefaultAsync())?.ToDomain();
		}

		protected override Event AddEntity()
		{
			var entity = new Event();
			DataContext.Add(entity);
			return entity;
		}

		protected override DbSet<Event> GetDbSet()
		{
			return DataContext.Events;
		}

		protected override void RemoveEntity(Event entity)
		{
			DataContext.Remove(entity);
		}
	}
}