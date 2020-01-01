using Microsoft.Extensions.DependencyInjection;

namespace StarCommander.Application.Tests
{
	public sealed class ServicesFixture
	{
		public ServicesFixture()
		{
			var services = new ServiceCollection();
			new TestSetup().ConfigureServices(services);
			ServiceProvider = services.BuildServiceProvider();
		}

		public ServiceProvider ServiceProvider { get; }
	}
}