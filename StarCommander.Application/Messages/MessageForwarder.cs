using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using StarCommander.Application.Services;
using StarCommander.Domain;
using StarCommander.Domain.Messages;
using Command = StarCommander.Domain.Messages.Command;

namespace StarCommander.Application.Messages
{
	public sealed class MessageForwarder : IMessageForwarder
	{
		readonly ICommandRepository commandRepository;
		readonly IDbContextScopeFactory dbContextScopeFactory;
		readonly IEventRepository eventRepository;
		readonly IReferenceGenerator generator;
		readonly IJobRepository jobRepository;
		readonly IJobScheduler jobScheduler;
		readonly IWorkerRegistry workerRegistry;

		public MessageForwarder(ICommandRepository commandRepository, IDbContextScopeFactory dbContextScopeFactory,
			IEventRepository eventRepository, IJobRepository jobRepository, IJobScheduler jobScheduler,
			IReferenceGenerator generator, IWorkerRegistry workerRegistry)
		{
			this.commandRepository = commandRepository;
			this.dbContextScopeFactory = dbContextScopeFactory;
			this.eventRepository = eventRepository;
			this.generator = generator;
			this.jobRepository = jobRepository;
			this.jobScheduler = jobScheduler;
			this.workerRegistry = workerRegistry;
		}

		public async Task<bool> ForwardNextMessage(CancellationToken cancellationToken)
		{
			return await ForwardNextCommand(cancellationToken) || await ForwardNextEvent(cancellationToken);
		}

		async Task<bool> ForwardNextCommand(CancellationToken cancellationToken)
		{
			var command = await FetchNextUnprocessedCommand();
			return command != null && await ForwardMessage(command, cancellationToken);
		}

		async Task<bool> ForwardNextEvent(CancellationToken cancellationToken)
		{
			var @event = await FetchNextUnprocessedEvent();
			return @event != null && await ForwardMessage(@event, cancellationToken);
		}

		async Task<bool> ForwardMessage<T>(Message<T> message, CancellationToken cancellationToken) where T :  IHaveType
		{
			var payload = message.Payload as IHaveType;

			var jobs = workerRegistry.GetHandlersFor(message.Payload)
				.Select(handler => new Job(generator.NewReference<Job>(), payload.GetAddress(), handler, message.Id,
					payload.GetQueueId(), message.Created))
				.ToList();

			using (var dbContextScope = dbContextScopeFactory.Create())
			{
				await jobRepository.SaveAll(jobs);

				message.MarkAsProcessed();

				switch (message)
				{
					case Command command:
						await commandRepository.Save(command);
						break;
					case Event @event:
						await eventRepository.Save(@event);
						break;
				}

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
				return await commandRepository.FetchNextUnprocessed();
			}
		}

		async Task<Event?> FetchNextUnprocessedEvent()
		{
			using (dbContextScopeFactory.CreateReadOnly())
			{
				return await eventRepository.FetchNextUnprocessed();
			}
		}
	}
}