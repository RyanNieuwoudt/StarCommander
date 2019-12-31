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
		public void CreateWithCorrectValues()
		{
			var id = fixture.Create<Reference<Player>>();
			var callSign = fixture.Create<string>();
			var firstName = fixture.Create<string>();
			var lastName = fixture.Create<string>();

			var player = Player.Create(id, callSign, firstName, lastName);

			Assert.Equal(id, player.Id);
			Assert.Equal(callSign, player.CallSign);
			Assert.Equal(firstName, player.FirstName);
			Assert.Equal(lastName, player.LastName);
		}
	}
}