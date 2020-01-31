using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using StarCommander.Application.Services;
using StarCommander.Domain.Messages;

namespace StarCommander.Application.Messages
{
	public class JobMonitor : BackgroundService
	{
		readonly IJobScheduler jobScheduler;
		readonly ILogger<JobMonitor> logger;
		readonly IServiceProvider serviceProvider;

		public JobMonitor(IJobScheduler jobScheduler, ILogger<JobMonitor> logger, IServiceProvider serviceProvider)
		{
			this.jobScheduler = jobScheduler;
			this.logger = logger;
			this.serviceProvider = serviceProvider;
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			logger.LogInformation("Starting job runner.");

			cancellationToken.Register(Stop);

			var existingJobs = await FetchExistingJobs();

			//TODO Way to ensure that new jobs cannot arrive ahead of existing jobs
			//as we start up.
			foreach (var job in existingJobs)
			{
				logger.LogInformation("Adding job.");
				jobScheduler.Add(job);
			}

			logger.LogInformation("Starting job scheduler");
			await jobScheduler.Start(cancellationToken);

			logger.LogInformation("No more work!");

			await jobScheduler.Stop();
		}

		void Stop()
		{
			logger.LogInformation("Stopping job runner.");
		}

		async Task<ICollection<Job>> FetchExistingJobs()
		{
			using var scope = serviceProvider.CreateScope();
			return await scope.ServiceProvider.GetService<IJobService>().Fetch();
		}
	}
}