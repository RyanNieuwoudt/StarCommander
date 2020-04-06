using AmbientDbContextConfigurator;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarCommander.Application;
using StarCommander.Application.Services;
using StarCommander.Hubs;
using StarCommander.Infrastructure.Persistence;

namespace StarCommander
{
	public class ApplicationSetup : CommonSetup
	{
		public ApplicationSetup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		IConfiguration Configuration { get; }

		protected override void ConfigureContextualServices(IServiceCollection services)
		{
			services.AddSingleton<IDbContextConfiguration>(new InMemoryConfiguration("StarCommander"));

			services.AddScoped<IChannelService, ChannelService>();
		}

		protected override void ConfigureDbContexts(IServiceCollection services)
		{
		}
	}
}