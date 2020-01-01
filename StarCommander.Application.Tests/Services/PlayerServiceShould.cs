using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using StarCommander.Application.Services;
using Xunit;

namespace StarCommander.Application.Tests.Services
{
	public class PlayerServiceShould : IClassFixture<ServicesFixture>
	{
		public PlayerServiceShould(ServicesFixture servicesFixture)
		{
			fixture = new Fixture();
			this.servicesFixture = servicesFixture;
		}

		readonly IFixture fixture;
		readonly ServicesFixture servicesFixture;

		[Fact]
		public async Task SignUpNewPlayers()
		{
			using var scope = servicesFixture.ServiceProvider.CreateScope();
			var playerService = scope.ServiceProvider.GetService<IPlayerService>();

			var callSign = fixture.Create<string>();
			var firstName = fixture.Create<string>();
			var lastName = fixture.Create<string>();

			var session = await playerService.SignUp(callSign, firstName, lastName);

			Assert.False(string.IsNullOrWhiteSpace(session.Token));
			Assert.Equal(callSign, session.Player.CallSign);
			Assert.Equal(firstName, session.Player.FirstName);
			Assert.Equal(lastName, session.Player.LastName);
		}
	}
}