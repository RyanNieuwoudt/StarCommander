using System;
using Microsoft.Extensions.DependencyInjection;
using StarCommander.Infrastructure.Persistence;

namespace StarCommander.Application.Tests
{
	public class TestSetup : CommonSetup
	{
		protected override void ConfigureContextualServices(IServiceCollection services)
		{
			services.Configure<AppSettings>(settings => settings.Secret = new string('1', 64));

			services.AddSingleton<IDbContextConfiguration>(new InMemoryConfiguration(Guid.NewGuid().ToString()));
		}

		protected override void ConfigureDbContexts(IServiceCollection services)
		{
		}
	}
}