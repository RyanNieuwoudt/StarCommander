using AutoMapper;
using EntityFramework.DbContextScope;
using EntityFramework.DbContextScope.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using StarCommander.Application.Events;
using StarCommander.Application.Services;
using StarCommander.Domain.Messages;
using StarCommander.Domain.Players;
using StarCommander.Infrastructure.Persistence;
using StarCommander.Infrastructure.Persistence.Aggregate.Messages;
using StarCommander.Infrastructure.Persistence.Aggregate.Players;

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
			services.AddScoped<IPlayerRepository, PlayerRepository>();

			services.AddScoped<IEventForwarder, EventForwarder>();
			services.AddScoped<IEventPublisher, EventPublisher>();
			services.AddSingleton<IJobScheduler, JobScheduler>();
			services.AddWorkerRegistry(typeof(CommonSetup));
			services.AddHostedService<EventMonitor>();
			services.AddHostedService<JobMonitor>();

			services.AddScoped<IJobService, JobService>();
			services.AddScoped<IPlayerService, PlayerService>();
			services.AddScoped<IReferenceGenerator, RandomIdGenerator>();
		}

		protected abstract void ConfigureContextualServices(IServiceCollection services);
	}
}