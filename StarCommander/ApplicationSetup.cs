using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarCommander.Application;
using StarCommander.Infrastructure.Persistence;

namespace StarCommander
{
	public sealed class ApplicationSetup : CommonSetup
	{
		public ApplicationSetup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		IConfiguration Configuration { get; }

		protected override void ConfigureContextualServices(IServiceCollection services)
		{
			services.AddSingleton<IDbContextConfiguration>(new InMemoryConfiguration("StarCommander"));
		}

		protected override void ConfigureDbContexts(IServiceCollection services)
		{
		}
	}
}