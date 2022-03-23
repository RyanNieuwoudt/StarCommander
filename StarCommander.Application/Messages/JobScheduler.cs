using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Polly;
using Polly.Retry;
using StarCommander.Application.Services;
using StarCommander.Domain.Messages;

namespace StarCommander.Application.Messages;

public sealed class JobScheduler : IJobScheduler
{
	const int PriorityQueues = 2;

	//TODO Limit number of retries, then move to poison queue?
	static readonly AsyncRetryPolicy RetryPolicy = Policy.Handle<Exception>()
		.WaitAndRetryForeverAsync(attempt => TimeSpan.FromMilliseconds(100 * Math.Min(attempt, 10)));

	//TODO Improve on this simple implementation
	static readonly string[] PriorityHandlers = { "StarCommander.Application.DomainEventHandlers.NotifyPlayer" };

	readonly ILogger<JobScheduler> logger;
	readonly BufferBlock<Job>[] queues;
	readonly IServiceProvider serviceProvider;

	readonly int standardQueues;

	public JobScheduler(ILogger<JobScheduler> logger, IServiceProvider serviceProvider)
	{
		this.logger = logger;
		this.serviceProvider = serviceProvider;

		standardQueues = Math.Max(2, Environment.ProcessorCount);

		queues = new BufferBlock<Job>[TotalQueues];
		for (var i = 0; i < queues.Length; i++)
		{
			queues[i] = new ();
		}
	}

	int TotalQueues => PriorityQueues + standardQueues;

	public void Add(Job job)
	{
		try
		{
			queues[CalculateBucket(job)].Post(job);
		}
		catch (Exception e)
		{
			logger.LogError(e, "Error adding job!");
			throw;
		}
	}

	public async Task Start(CancellationToken cancellationToken, bool autoStop = false)
	{
		var tasks = new List<Task>();

		for (var i = 0; i < TotalQueues; i++)
		{
			var index = i;

			if (autoStop)
			{
				tasks.Add(Task.Factory.StartNew(async () =>
					{
						while (queues[index].Count > 0)
						{
							var job = await queues[index].ReceiveAsync(cancellationToken);
							logger.LogInformation($"Starting job {job.Id}.");
							await RetryPolicy.ExecuteAsync(() => Run(job, cancellationToken));
							logger.LogInformation($"Job {job.Id} finished.");
						}
					}, cancellationToken, TaskCreationOptions.LongRunning, TaskScheduler.Default)
				);
			}
			else
			{
				tasks.Add(Task.Factory.StartNew(async () =>
						{
							while (await queues[index].OutputAvailableAsync(cancellationToken))
							{
								var job = await queues[index].ReceiveAsync(cancellationToken);
								logger.LogInformation($"Starting job {job.Id}.");
								await RetryPolicy.ExecuteAsync(() => Run(job, cancellationToken));
								logger.LogInformation($"Job {job.Id} finished.");
							}
						}, cancellationToken, TaskCreationOptions.DenyChildAttach | TaskCreationOptions.LongRunning,
						TaskScheduler.Default)
					.Unwrap()
				);
			}
		}

		await Task.WhenAll(tasks);
	}

	public Task Stop()
	{
		for (var i = 0; i < TotalQueues; i++)
		{
			queues[i].Complete();
		}

		return Task.CompletedTask;
	}

	int CalculateBucket(Job job)
	{
		//TODO Revisit whether using GetHashCode() is advisable.
		return PriorityHandlers.Contains(job.Handler)
			? Math.Abs(job.QueueId.GetHashCode()) % PriorityQueues
			: Math.Abs(job.QueueId.GetHashCode()) % standardQueues + PriorityQueues;
	}

	async Task Run(Job job, CancellationToken cancellationToken)
	{
		try
		{
			logger.LogInformation($"Running job {job.Id}.");
			using var scope = serviceProvider.CreateScope();
			await scope.ServiceProvider.GetService<IJobService>()!.Run(job, cancellationToken);
		}
		catch (Exception e)
		{
			logger.LogError(e, $"Error running job {job.Id}.");
			throw;
		}
	}
}