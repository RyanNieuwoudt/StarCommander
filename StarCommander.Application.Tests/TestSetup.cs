using System;
using Microsoft.Extensions.DependencyInjection;
using StarCommander.Infrastructure.Persistence;

namespace StarCommander.Application.Tests
{
	public sealed class TestSetup : CommonSetup
	{
		protected override void ConfigureContextualServices(IServiceCollection services)
		{
			services.AddSingleton<IDbContextConfiguration>(new InMemoryConfiguration(Guid.NewGuid().ToString()));
		}

		protected override void ConfigureDbContexts(IServiceCollection services)
		{
		}
	}
}