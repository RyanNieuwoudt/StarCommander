using AutoMapper;
using EntityFramework.DbContextScope;
using EntityFramework.DbContextScope.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using StarCommander.Application.Messages;
using StarCommander.Application.Services;
using StarCommander.Domain.Messages;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;
using StarCommander.Infrastructure.Persistence;
using StarCommander.Infrastructure.Persistence.Aggregate.Messages;
using StarCommander.Infrastructure.Persistence.Aggregate.Players;
using StarCommander.Infrastructure.Persistence.Aggregate.Ships;

namespace StarCommander.Application
{
	public abstract class CommonSetup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			ConfigureDbContexts(services);
			ConfigureCommonServices(services);
			ConfigureContextualServices(services);
		}

		protected abstract void ConfigureDbContexts(IServiceCollection services);

		static void ConfigureCommonServices(IServiceCollection services)
		{
			services.AddAutoMapper(typeof(AutoMapperProfile));

			services.AddScoped<IAmbientDbContextConfigurator, AmbientDbContextConfigurator>();
			services.AddScoped<IAmbientDbContextLocator, AmbientDbContextLocator>();
			services.AddScoped<IDbContextScopeFactory, DbContextScopeFactory>();

			services.AddScoped<IEventRepository, EventRepository>();
			services.AddScoped<IJobRepository, JobRepository>();
			services.AddScoped<IPlayerCommandRepository, PlayerCommandRepository>();
			services.AddScoped<IPlayerRepository, PlayerRepository>();
			services.AddScoped<IShipCommandRepository, ShipCommandRepository>();
			services.AddScoped<IShipRepository, ShipRepository>();

			services.AddScoped<IMessageForwarder, MessageForwarder>();
			services.AddScoped<IEventPublisher, EventPublisher>();
			services.AddSingleton<IJobScheduler, JobScheduler>();
			services.AddWorkerRegistry(typeof(CommonSetup));
			services.AddHostedService<JobMonitor>();
			services.AddHostedService<MessageMonitor>();

			services.AddScoped<ICommandService, CommandService>();
			services.AddScoped<IJobService, JobService>();
			services.AddScoped<IPlayerService, PlayerService>();
			services.AddScoped<IReferenceGenerator, RandomIdGenerator>();
			services.AddScoped<IShipService, ShipService>();
		}

		protected abstract void ConfigureContextualServices(IServiceCollection services);
	}
}