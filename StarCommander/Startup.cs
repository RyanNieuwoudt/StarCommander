using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using StarCommander.Application;
using StarCommander.Hubs;
using StarCommander.Middleware.ExceptionHandling;
using static StarCommander.Shared.Communication.Hubs;

namespace StarCommander;

public class Startup
{
	readonly IConfiguration configuration;

	public Startup(IConfiguration configuration) => this.configuration = configuration;

	public void ConfigureServices(IServiceCollection services)
	{
		new ApplicationSetup(configuration).ConfigureServices(services);

		services.AddControllers().AddNewtonsoftJson();
		services.AddSignalR().AddMessagePackProtocol();

		// In production, the React files will be served from this directory
		services.AddSpaStaticFiles(configuration => { configuration.RootPath = "ClientApp/build"; });

		var appSettingsSection = configuration.GetSection("AppSettings");
		services.Configure<AppSettings>(appSettingsSection);

		var appSettings = appSettingsSection.Get<AppSettings>();
		var key = Encoding.ASCII.GetBytes(appSettings.Secret);
		services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(x =>
			{
				x.RequireHttpsMetadata = false;
				x.SaveToken = true;
				x.TokenValidationParameters = new()
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false
				};

				x.Events = new()
				{
					OnMessageReceived = context =>
					{
						var accessToken = context.Request.Query["access_token"];
						if (!string.IsNullOrEmpty(accessToken) &&
						    context.HttpContext.Request.Path.StartsWithSegments(HubRoot))
						{
							context.Token = accessToken;
						}

						return Task.CompletedTask;
					}
				};
			});
	}

	public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
	{
		if (env.IsDevelopment())
		{
			app.UseDeveloperExceptionHandlerMiddleware();
		}
		else
		{
			app.UseExceptionHandlerMiddleware();
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();
		app.UseSpaStaticFiles();

		app.UseRouting();

		app.UseAuthentication();
		app.UseAuthorization();

		app.UseEndpoints(endpoints =>
		{
			endpoints.MapHub<ChannelHub>(ChannelHubPath).RequireAuthorization();
			endpoints.MapControllers();
		});

		app.UseSpa(spa =>
		{
			spa.Options.SourcePath = "ClientApp";
			if (env.IsDevelopment())
			{
				spa.UseReactDevelopmentServer("start");
			}
		});
	}
}