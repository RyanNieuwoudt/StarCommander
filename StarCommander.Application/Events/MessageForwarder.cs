using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using StarCommander.Application.Services;
using StarCommander.Domain;
using StarCommander.Domain.Messages;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;
using static StarCommander.Domain.Messages.Job;
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
		readonly IShipCommandRepository shipCommandRepository;
		readonly IWorkerRegistry workerRegistry;

		public MessageForwarder(IDbContextScopeFactory dbContextScopeFactory, IEventRepository eventRepository,
			IReferenceGenerator generator, IJobRepository jobRepository, IJobScheduler jobScheduler,
			IPlayerCommandRepository playerCommandRepository, IWorkerRegistry workerRegistry,
			IShipCommandRepository shipCommandRepository)
		{
			this.dbContextScopeFactory = dbContextScopeFactory;
			this.eventRepository = eventRepository;
			this.generator = generator;
			this.jobRepository = jobRepository;
			this.jobScheduler = jobScheduler;
			this.playerCommandRepository = playerCommandRepository;
			this.shipCommandRepository = shipCommandRepository;
			this.workerRegistry = workerRegistry;
		}

		public async Task<bool> ForwardNextMessage(CancellationToken cancellationToken)
		{
			return await ForwardNextPlayerCommand(cancellationToken) ||
			       await ForwardNextShipCommand(cancellationToken) ||
			       await ForwardNextEvent(cancellationToken);
		}

		async Task<bool> ForwardNextPlayerCommand(CancellationToken cancellationToken)
		{
			var command = await FetchNextUnprocessedPlayerCommand();

			if (command == null)
			{
				return false;
			}

			var queueId = GetQueueId(command.Payload);

			var jobs = workerRegistry.GetHandlersFor(command.Payload)
				.Select(handler => new Job(generator.NewReference<Job>(), queueId, command.Id, PlayerCommands, handler,
					command.Created))
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

		async Task<Command?> FetchNextUnprocessedPlayerCommand()
		{
			using (dbContextScopeFactory.CreateReadOnly())
			{
				return await playerCommandRepository.FetchNextUnprocessed();
			}
		}

		async Task<bool> ForwardNextShipCommand(CancellationToken cancellationToken)
		{
			var command = await FetchNextUnprocessedShipCommand();

			if (command == null)
			{
				return false;
			}

			var queueId = GetQueueId(command.Payload);

			var jobs = workerRegistry.GetHandlersFor(command.Payload)
				.Select(handler => new Job(generator.NewReference<Job>(), queueId, command.Id, ShipCommands, handler,
					command.Created))
				.ToList();

			using (var dbContextScope = dbContextScopeFactory.Create())
			{
				await jobRepository.SaveAll(jobs);

				command.MarkAsProcessed();
				await shipCommandRepository.Save(command);

				await dbContextScope.SaveChangesAsync(cancellationToken);
			}

			foreach (var job in jobs)
			{
				jobScheduler.Add(job);
			}

			return true;
		}

		async Task<Command?> FetchNextUnprocessedShipCommand()
		{
			using (dbContextScopeFactory.CreateReadOnly())
			{
				return await shipCommandRepository.FetchNextUnprocessed();
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
				.Select(handler => new Job(generator.NewReference<Job>(), queueId, @event.Id, DomainEvents, handler,
					@event.Created))
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