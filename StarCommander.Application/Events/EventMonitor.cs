using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace StarCommander.Application.Events
{
	public class EventMonitor : BackgroundService
	{
		readonly ILogger<EventMonitor> logger;
		readonly IServiceProvider serviceProvider;

		public EventMonitor(ILogger<EventMonitor> logger, IServiceProvider serviceProvider)
		{
			this.logger = logger;
			this.serviceProvider = serviceProvider;
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			logger.LogInformation("Starting event monitor.");

			cancellationToken.Register(Stop);

			while (!cancellationToken.IsCancellationRequested)
			{
				//TODO Ideally should only delay after an appropriate idle period...
				if (!await DoWork(cancellationToken))
				{
					await Task.Delay(100, cancellationToken);
				}
			}
		}

		async Task<bool> DoWork(CancellationToken cancellationToken)
		{
			try
			{
				using var scope = serviceProvider.CreateScope();
				return await scope.ServiceProvider.GetService<IMessageForwarder>().ForwardNextMessage(cancellationToken);
			}
			catch (Exception e)
			{
				logger.LogError(e.Message);
				return false;
			}
		}

		void Stop()
		{
			logger.LogInformation("Stopping event monitor.");
		}
	}
}