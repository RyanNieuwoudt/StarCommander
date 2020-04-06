using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace StarCommander
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			await CreateHostBuilder(args).Build().Migrate().RunAsync();
		}

		public static IHostBuilder CreateHostBuilder(string[] args)
		{
			return Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.ConfigureAppConfiguration((hostingContext, config) =>
						{
							config.AddEnvironmentVariables("StarCommander_");
						})
						.UseDefaultServiceProvider((context, options) =>
						{
							options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
							options.ValidateOnBuild = true;
						})
						.UseStartup<Startup>();
				});
		}
	}
}