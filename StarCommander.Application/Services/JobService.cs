using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using StarCommander.Domain;
using StarCommander.Domain.Messages;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;
using Command = StarCommander.Domain.Messages.Command;

namespace StarCommander.Application.Services
{
	public sealed class JobService : IJobService
	{
		readonly ICommandRepository commandRepository;
		readonly IDbContextScopeFactory dbContextScopeFactory;
		readonly IEventRepository eventRepository;
		readonly IJobRepository jobRepository;
		readonly IServiceProvider serviceProvider;

		public JobService(ICommandRepository commandRepository, IDbContextScopeFactory dbContextScopeFactory,
			IEventRepository eventRepository, IJobRepository jobRepository, IServiceProvider serviceProvider)
		{
			this.commandRepository = commandRepository;
			this.dbContextScopeFactory = dbContextScopeFactory;
			this.eventRepository = eventRepository;
			this.jobRepository = jobRepository;
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

			using var dbContextScope = dbContextScopeFactory.Create();

			switch (job.Address)
			{
				case Job.Commands:
					var command = await commandRepository.Fetch(new Reference<Command>(job.MessageId));
					await Handle(handlerType, command, cancellationToken);
					break;
				case Job.DomainEvents:
					var @event = await eventRepository.Fetch(new Reference<Event>(job.MessageId));
					await Handle(handlerType, @event, cancellationToken);
					break;
			}

			await jobRepository.Remove(job);

			await dbContextScope.SaveChangesAsync(cancellationToken);
		}

		async Task Handle<T>(Type handlerType, Message<T> message, CancellationToken cancellationToken)
			where T : notnull, IHaveType
		{
			using var scope = serviceProvider.CreateScope();
			var handler = scope.ServiceProvider.GetService(handlerType);
			var arguments = new object[] { message.Payload, cancellationToken };
			var method = handlerType.GetMethod("Handle", arguments.Select(a => a.GetType()).ToArray());

			if (method == null)
			{
				throw new InvalidOperationException();
			}

			if (method.Invoke(handler, arguments) is Task task)
			{
				await task;
			}
		}
	}
}