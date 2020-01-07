using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using StarCommander.Application.CommandHandlers;
using StarCommander.Application.DomainEventHandlers;
using StarCommander.Domain;
using StarCommander.Domain.Messages;
using StarCommander.Domain.Players;

namespace StarCommander.Application.Services
{
	public class JobService : IJobService
	{
		readonly IDbContextScopeFactory dbContextScopeFactory;
		readonly IEventRepository eventRepository;
		readonly IJobRepository jobRepository;
		readonly IPlayerCommandRepository playerCommandRepository;
		readonly IServiceProvider serviceProvider;

		public JobService(IDbContextScopeFactory dbContextScopeFactory, IEventRepository eventRepository,
			IJobRepository jobRepository, IPlayerCommandRepository playerCommandRepository,
			IServiceProvider serviceProvider)
		{
			this.dbContextScopeFactory = dbContextScopeFactory;
			this.eventRepository = eventRepository;
			this.jobRepository = jobRepository;
			this.playerCommandRepository = playerCommandRepository;
			this.serviceProvider = serviceProvider;
		}

		public async Task<ICollection<Job>> Fetch()
		{
			using (dbContextScopeFactory.CreateReadOnly())
			{
				return await jobRepository.All();
			}
		}

		public async Task Run(Job job, CancellationToken cancellationToken)
		{
			var handlerType = Type.GetType(job.Handler);

			if (handlerType == null)
			{
				throw new InvalidOperationException();
			}

			using var scope = serviceProvider.CreateScope();
			var handler = scope.ServiceProvider.GetService(handlerType);

			using var dbContextScope = dbContextScopeFactory.Create();

			switch (handler)
			{
				case IObey _:
				{
					var command =
						await playerCommandRepository.Fetch(new Reference<Domain.Messages.Command>(job.MessageId));
					await Handle(handlerType, command, cancellationToken);
					break;
				}
				case IWhen _:
				{
					var @event = await eventRepository.Fetch(new Reference<Event>(job.MessageId));
					await Handle(handlerType, @event, cancellationToken);
					break;
				}
			}

			await jobRepository.Remove(job);

			await dbContextScope.SaveChangesAsync(cancellationToken);
		}

		async Task Handle(Type handlerType, Domain.Messages.Command command, CancellationToken cancellationToken)
		{
			using var scope = serviceProvider.CreateScope();
			var handler = scope.ServiceProvider.GetService(handlerType);

			var method =
				handlerType.GetMethod("Handle", new[] { command.Payload.GetType(), typeof(CancellationToken) });

			if (method == null)
			{
				throw new InvalidOperationException();
			}

			if (method.Invoke(handler, new object[] { command.Payload, cancellationToken }) is Task
				task)
			{
				await task;
			}
		}

		async Task Handle(Type handlerType, Event @event, CancellationToken cancellationToken)
		{
			using var scope = serviceProvider.CreateScope();
			var handler = scope.ServiceProvider.GetService(handlerType);

			var method = handlerType.GetMethod("Handle", new[] { @event.Payload.GetType(), typeof(CancellationToken) });

			if (method == null)
			{
				throw new InvalidOperationException();
			}

			if (method.Invoke(handler, new object[] { @event.Payload, cancellationToken }) is Task task)
			{
				await task;
			}
		}
	}
}