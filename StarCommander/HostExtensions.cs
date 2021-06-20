using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StarCommander.Infrastructure.Persistence.Aggregate.Messages;
using StarCommander.Infrastructure.Persistence.Aggregate.Players;
using StarCommander.Infrastructure.Persistence.Aggregate.Ships;
using StarCommander.Infrastructure.Persistence.Projection;

namespace StarCommander
{
	static class HostExtensions
	{
		internal static IHost Migrate(this IHost host)
		{
			static void Migrate<T>(IServiceScope scope) where T : DbContext
			{
				using var dbContext = scope.ServiceProvider.GetService<T>();

				dbContext?.Database.Migrate();
			}

			using (var scope = host.Services.GetService<IServiceScopeFactory>()!.CreateScope())
			{
				Migrate<MessageDataContext>(scope);
				Migrate<PlayerDataContext>(scope);
				Migrate<ShipDataContext>(scope);

				Migrate<ProjectionDataContext>(scope);
			}

			return host;
		}
	}
}