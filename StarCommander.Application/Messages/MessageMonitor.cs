using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace StarCommander.Application.Messages
{
	public class MessageMonitor : BackgroundService
	{
		readonly ILogger<MessageMonitor> logger;
		readonly IServiceProvider serviceProvider;

		public MessageMonitor(ILogger<MessageMonitor> logger, IServiceProvider serviceProvider)
		{
			this.logger = logger;
			this.serviceProvider = serviceProvider;
		}

		protected override async Task ExecuteAsync(CancellationToken cancellationToken)
		{
			logger.LogInformation("Starting message monitor.");

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
				return await scope.ServiceProvider.GetService<IMessageForwarder>()
					.ForwardNextMessage(cancellationToken);
			}
			catch (Exception e)
			{
				logger.LogError(e.Message);
				return false;
			}
		}

		void Stop()
		{
			logger.LogInformation("Stopping message monitor.");
		}
	}
}