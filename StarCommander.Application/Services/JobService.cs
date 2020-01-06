using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using StarCommander.Domain;
using StarCommander.Domain.Messages;

namespace StarCommander.Application.Services
{
	public class JobService : IJobService
	{
		readonly IDbContextScopeFactory dbContextScopeFactory;
		readonly IEventRepository eventRepository;
		readonly IJobRepository jobRepository;
		readonly IServiceProvider serviceProvider;

		public JobService(IDbContextScopeFactory dbContextScopeFactory, IEventRepository eventRepository,
			IJobRepository jobRepository, IServiceProvider serviceProvider)
		{
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
			using var dbContextScope = dbContextScopeFactory.Create();

			var @event = await eventRepository.Fetch(new Reference<Event>(job.MessageId));

			await HandleEvent(job, @event, cancellationToken);
			await jobRepository.Remove(job);

			await dbContextScope.SaveChangesAsync(cancellationToken);
		}

		async Task HandleEvent(Job job, Event @event, CancellationToken cancellationToken)
		{
			var eventHandlerType = Type.GetType(job.Handler);

			if (eventHandlerType == null)
			{
				throw new InvalidOperationException();
			}

			using var scope = serviceProvider.CreateScope();
			var eventHandler = scope.ServiceProvider.GetService(eventHandlerType);

			var method =
				eventHandlerType.GetMethod("Handle", new[] { @event.Payload.GetType(), typeof(CancellationToken) });

			if (method == null)
			{
				throw new InvalidOperationException();
			}

			if (method.Invoke(eventHandler, new object[] { @event.Payload, cancellationToken }) is Task task)
			{
				await task;
			}
		}
	}
}