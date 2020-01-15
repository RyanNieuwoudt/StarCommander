using System.Linq;
using AutoFixture;
using StarCommander.Domain.Players;
using Xunit;

namespace StarCommander.Domain.Tests.Players
{
	public class PlayerShould
	{
		public PlayerShould()
		{
			fixture = new Fixture();
		}

		readonly IFixture fixture;

		[Fact]
		public void RaiseEventOnSignIn()
		{
			var id = fixture.Create<Reference<Player>>();
			var callSign = fixture.Create<string>();
			var firstName = fixture.Create<string>();
			var lastName = fixture.Create<string>();

			var player = Player.SignUp(id, callSign, firstName, lastName, new byte[0], new byte[0]);

			player.SignIn();

			var playerSignedIn = player.Events.Single(e => e is PlayerSignedIn) as PlayerSignedIn;

			Assert.NotNull(playerSignedIn);
			Assert.Equal(player.Reference, playerSignedIn!.Player);
		}

		[Fact]
		public void RaiseEventOnSignUp()
		{
			var id = fixture.Create<Reference<Player>>();
			var callSign = fixture.Create<string>();
			var firstName = fixture.Create<string>();
			var lastName = fixture.Create<string>();

			var player = Player.SignUp(id, callSign, firstName, lastName, new byte[0], new byte[0]);

			var playerSignedUp = player.Events.Single(e => e is PlayerSignedUp) as PlayerSignedUp;

			Assert.NotNull(playerSignedUp);
			Assert.Equal(player.Reference, playerSignedUp!.Player);
		}

		[Fact]
		public void SignUpWithCorrectValues()
		{
			var id = fixture.Create<Reference<Player>>();
			var callSign = fixture.Create<string>();
			var firstName = fixture.Create<string>();
			var lastName = fixture.Create<string>();

			var player = Player.SignUp(id, callSign, firstName, lastName, new byte[0], new byte[0]);

			Assert.Equal(id, player.Id);
			Assert.Equal(callSign, player.CallSign);
			Assert.Equal(firstName, player.FirstName);
			Assert.Equal(lastName, player.LastName);
		}

		[Fact]
		public void UpdateName()
		{
			var firstName = fixture.Create<string>();
			var lastName = fixture.Create<string>();

			var player = Player.SignUp(fixture.Create<Reference<Player>>(), fixture.Create<string>(), firstName,
				lastName, new byte[0], new byte[0]);

			Assert.Equal(firstName, player.FirstName);
			Assert.Equal(lastName, player.LastName);

			var newFirstName = fixture.Create<string>();
			var newLastName = fixture.Create<string>();

			Assert.NotEqual(firstName, newFirstName);
			Assert.NotEqual(lastName, newLastName);

			player.UpdateName(newFirstName, newLastName);

			Assert.Equal(newFirstName, player.FirstName);
			Assert.Equal(newLastName, player.LastName);

			var playerNameChanged = player.Events.Single(e => e is PlayerNameChanged) as PlayerNameChanged;

			Assert.NotNull(playerNameChanged);
			Assert.Equal(player.Reference, playerNameChanged!.Player);
			Assert.Equal(player.FirstName, playerNameChanged.FirstName);
			Assert.Equal(player.LastName, playerNameChanged.LastName);
		}
	}
}