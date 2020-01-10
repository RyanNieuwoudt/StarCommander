using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using StarCommander.Application.Services;
using StarCommander.Domain;
using StarCommander.Domain.Messages;
using StarCommander.Domain.Players;
using Command = StarCommander.Domain.Messages.Command;

namespace StarCommander.Application.Events
{
	public class MessageForwarder : IMessageForwarder
	{
		readonly IDbContextScopeFactory dbContextScopeFactory;
		readonly IEventRepository eventRepository;
		readonly IReferenceGenerator generator;
		readonly IJobRepository jobRepository;
		readonly IJobScheduler jobScheduler;
		readonly IPlayerCommandRepository playerCommandRepository;
		readonly IWorkerRegistry workerRegistry;

		public MessageForwarder(IDbContextScopeFactory dbContextScopeFactory, IEventRepository eventRepository,
			IReferenceGenerator generator, IJobRepository jobRepository, IJobScheduler jobScheduler,
			IPlayerCommandRepository playerCommandRepository, IWorkerRegistry workerRegistry)
		{
			this.dbContextScopeFactory = dbContextScopeFactory;
			this.eventRepository = eventRepository;
			this.generator = generator;
			this.jobRepository = jobRepository;
			this.jobScheduler = jobScheduler;
			this.playerCommandRepository = playerCommandRepository;
			this.workerRegistry = workerRegistry;
		}

		public async Task<bool> ForwardNextMessage(CancellationToken cancellationToken)
		{
			var c = await ForwardNextCommand(cancellationToken);
			var e = await ForwardNextEvent(cancellationToken);

			return c || e;
		}

		async Task<bool> ForwardNextCommand(CancellationToken cancellationToken)
		{
			var command = await FetchNextUnprocessedCommand();

			if (command == null)
			{
				return false;
			}

			var queueId = GetQueueId(command.Payload);

			var jobs = workerRegistry.GetHandlersFor(command.Payload)
				.Select(handler =>
					new Job(generator.NewReference<Job>(), queueId, command.Id, handler, command.Created))
				.ToList();

			using (var dbContextScope = dbContextScopeFactory.Create())
			{
				await jobRepository.SaveAll(jobs);

				command.MarkAsProcessed();
				await playerCommandRepository.Save(command);

				await dbContextScope.SaveChangesAsync(cancellationToken);
			}

			foreach (var job in jobs)
			{
				jobScheduler.Add(job);
			}

			return true;
		}

		async Task<Command?> FetchNextUnprocessedCommand()
		{
			using (dbContextScopeFactory.CreateReadOnly())
			{
				return await playerCommandRepository.FetchNextUnprocessed();
			}
		}

		static Guid GetQueueId(ICommand command)
		{
			return command switch { _ => Guid.Empty };
		}

		async Task<bool> ForwardNextEvent(CancellationToken cancellationToken)
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

		async Task<Event?> FetchNextUnprocessedEvent()
		{
			using (dbContextScopeFactory.CreateReadOnly())
			{
				return await eventRepository.FetchNextUnprocessed();
			}
		}

		static Guid GetQueueId(IDomainEvent @event)
		{
			return @event switch
			{
				_ => Guid.Empty
			};
		}
	}
}