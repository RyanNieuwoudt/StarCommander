using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using StarCommander.Application.Services;
using StarCommander.Domain;
using StarCommander.Domain.Messages;

namespace StarCommander.Application.Messages;

public class EventPublisher : IEventPublisher
{
	readonly IDbContextScopeFactory dbContextScopeFactory;
	readonly IEventRepository eventRepository;
	readonly IReferenceGenerator generator;

	public EventPublisher(IDbContextScopeFactory dbContextScopeFactory, IEventRepository eventRepository,
		IReferenceGenerator generator)
	{
		this.dbContextScopeFactory = dbContextScopeFactory;
		this.eventRepository = eventRepository;
		this.generator = generator;
	}

	public async Task Publish(params IRaiseDomainEvents[] aggregates)
	{
		using var dbContextScope = dbContextScopeFactory.Create();
		foreach (var aggregate in aggregates)
		{
			foreach (var domainEvent in aggregate.Events)
			{
				await eventRepository.Save(Event.Wrap(generator.NewReference<Message<IDomainEvent>>(),
					domainEvent));
			}
		}

		await dbContextScope.SaveChangesAsync();
	}
}