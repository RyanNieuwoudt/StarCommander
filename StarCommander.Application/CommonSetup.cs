using EntityFramework.DbContextScope;
using EntityFramework.DbContextScope.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using StarCommander.Infrastructure.Persistence;

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
		}

		protected abstract void ConfigureContextualServices(IServiceCollection services);
	}
}