using System.Threading.Tasks;
using AmbientDbContextConfigurator;
using EntityFramework.DbContextScope.Interfaces;
using StarCommander.Domain;
using StarCommander.Domain.Messages;

namespace StarCommander.Infrastructure.Persistence;

public abstract class EventsOnlyRepository<T, TV> : RepositoryBase<TV>
	where T : class, IRaiseDomainEvents where TV : class, IDbContext
{
	readonly IEventPublisher eventPublisher;

	protected EventsOnlyRepository(IAmbientDbContextConfigurator ambientDbContextConfigurator,
		IEventPublisher eventPublisher) : base(ambientDbContextConfigurator) => this.eventPublisher = eventPublisher;

	public async Task Save(T aggregate)
	{
		await eventPublisher.Publish(aggregate);
		aggregate.ClearEvents();
	}

	public async Task Remove(T aggregate)
	{
		await eventPublisher.Publish(aggregate);
		aggregate.ClearEvents();
	}
}