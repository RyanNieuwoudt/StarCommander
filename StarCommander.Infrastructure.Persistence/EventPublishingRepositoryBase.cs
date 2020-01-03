using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using StarCommander.Domain;
using StarCommander.Domain.Messages;

namespace StarCommander.Infrastructure.Persistence
{
	public abstract class EventPublishingRepositoryBase<T, T1, TU, TV> : JsonRepositoryBase<T, T1, TU, TV>
		where T : class, IAggregate, IRaiseDomainEvents
		where T1 : class, T
		where TU : JsonEntity<T, T1>
		where TV : class, IDbContext
	{
		readonly IEventPublisher eventPublisher;

		protected EventPublishingRepositoryBase(IAmbientDbContextConfigurator ambientDbContextConfigurator,
			IEventPublisher eventPublisher) : base(ambientDbContextConfigurator)
		{
			this.eventPublisher = eventPublisher;
		}

		public override async Task Save(T aggregate)
		{
			await base.Save(aggregate);
			await eventPublisher.Publish(aggregate);
			aggregate.ClearEvents();
		}

		public override async Task Remove(T aggregate)
		{
			await base.Remove(aggregate);
			await eventPublisher.Publish(aggregate);
			aggregate.ClearEvents();
		}
	}
}