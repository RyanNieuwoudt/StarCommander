using EntityFramework.DbContextScope;
using EntityFramework.DbContextScope.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using StarCommander.Application.Services;
using StarCommander.Domain.Players;
using StarCommander.Infrastructure.Persistence;
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
			services.AddScoped<IAmbientDbContextConfigurator, AmbientDbContextConfigurator>();
			services.AddScoped<IAmbientDbContextLocator, AmbientDbContextLocator>();
			services.AddScoped<IDbContextScopeFactory, DbContextScopeFactory>();

			services.AddScoped<IPlayerRepository, PlayerRepository>();

			services.AddScoped<IPlayerService, PlayerService>();
		}

		protected abstract void ConfigureContextualServices(IServiceCollection services);
	}
}