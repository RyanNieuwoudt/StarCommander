using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using StarCommander.Application.Services;
using StarCommander.Domain;
using StarCommander.Domain.Messages;

namespace StarCommander.Application.Events
{
	public class EventForwarder : IEventForwarder
	{
		readonly IDbContextScopeFactory dbContextScopeFactory;
		readonly IEventRepository eventRepository;
		readonly IReferenceGenerator generator;
		readonly IJobRepository jobRepository;
		readonly IJobScheduler jobScheduler;
		readonly IWorkerRegistry workerRegistry;

		public EventForwarder(IDbContextScopeFactory dbContextScopeFactory, IEventRepository eventRepository,
			IReferenceGenerator generator, IJobRepository jobRepository, IJobScheduler jobScheduler,
			IWorkerRegistry workerRegistry)
		{
			this.dbContextScopeFactory = dbContextScopeFactory;
			this.eventRepository = eventRepository;
			this.generator = generator;
			this.jobRepository = jobRepository;
			this.jobScheduler = jobScheduler;
			this.workerRegistry = workerRegistry;
		}

		public async Task<bool> ForwardNextEvent(CancellationToken cancellationToken)
		{
			var @event = await FetchNextUnprocessedEvent();

			if (@event == null)
			{
				return false;
			}

			var queueId = GetQueueId(@event.Payload);

			var jobs = workerRegistry.GetHandlersFor(@event.Payload)
				.Select(handler => new Job(generator.NewReference<Job>(), queueId, @event.Id, handler, @event.Created))
				.ToList();

			using (var dbContextScope = dbContextScopeFactory.Create())
			{
				await jobRepository.SaveAll(jobs);

				@event.MarkAsProcessed();
				await eventRepository.Save(@event);

				await dbContextScope.SaveChangesAsync(cancellationToken);
			}

			foreach (var job in jobs)
			{
				jobScheduler.Add(job);
			}

			return true;
		}

		async Task<Event> FetchNextUnprocessedEvent()
		{
			using (dbContextScopeFactory.CreateReadOnly())
			{
				return await eventRepository.FetchNextUnprocessed();
			}
		}

		static Guid GetQueueId(IDomainEvent @event)
		{
			return @event switch { _ => Guid.Empty };
		}
	}
}