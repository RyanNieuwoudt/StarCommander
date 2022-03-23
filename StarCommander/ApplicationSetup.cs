using AmbientDbContextConfigurator;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StarCommander.Application;
using StarCommander.Application.Services;
using StarCommander.Hubs;
using StarCommander.Infrastructure.Persistence;
using StarCommander.Infrastructure.Persistence.Aggregate.Messages;
using StarCommander.Infrastructure.Persistence.Aggregate.Players;
using StarCommander.Infrastructure.Persistence.Aggregate.Ships;
using StarCommander.Infrastructure.Persistence.Projection;

namespace StarCommander;

public class ApplicationSetup : CommonSetup
{
	public ApplicationSetup(IConfiguration configuration)
	{
		Configuration = configuration;
	}

	IConfiguration Configuration { get; }

	string ConnectionString => Configuration.GetConnectionString("StarCommander");

	protected override void ConfigureContextualServices(IServiceCollection services)
	{
		if (string.IsNullOrWhiteSpace(ConnectionString))
		{
			services.AddSingleton<IDbContextConfiguration>(new InMemoryConfiguration("StarCommander"));
		}
		else
		{
			services.AddSingleton<IDbContextConfiguration>(new PostgresConfiguration(ConnectionString));
		}

		services.AddScoped<IChannelService, ChannelService>();
	}

	protected override void ConfigureDbContexts(IServiceCollection services)
	{
		if (string.IsNullOrWhiteSpace(ConnectionString))
		{
			return;
		}

		void AddDbContext<T>(string migrationsHistoryTable) where T : DbContext
		{
			services.AddDbContext<T>(opt => opt.UseNpgsql(ConnectionString,
				b => { b.MigrationsHistoryTable(migrationsHistoryTable).MigrationsAssembly("StarCommander"); }));
		}

		//Aggregates
		AddDbContext<MessageDataContext>("MessageMigrations");
		AddDbContext<PlayerDataContext>("PlayerMigrations");
		AddDbContext<ShipDataContext>("ShipMigrations");

		//Projections
		AddDbContext<ProjectionDataContext>("ProjectionMigrations");
	}
}