using System.Linq;
using AutoFixture;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;
using Xunit;

namespace StarCommander.Domain.Tests.Ships;

public class ShipShould
{
	readonly IFixture fixture;

	public ShipShould()
	{
		fixture = new Fixture();
	}

	[Fact]
	public void LaunchWithCorrectValues()
	{
		var id = fixture.Create<Reference<Ship>>();
		var player = fixture.Create<Reference<Player>>();

		var ship = Ship.Launch(id, player);

		Assert.Equal(ship.Reference, id);
		Assert.Equal(ship.Captain, player);
	}

	[Fact]
	public void RaiseEventOnLaunch()
	{
		var id = fixture.Create<Reference<Ship>>();
		var player = fixture.Create<Reference<Player>>();

		var ship = Ship.Launch(id, player);

		var shipLaunched = ship.Events.Single(e => e is ShipLaunched) as ShipLaunched;

		Assert.NotNull(shipLaunched);
		Assert.Equal(ship.Reference, shipLaunched!.Ship);
		Assert.Equal(ship.Captain, shipLaunched!.Player);
	}
}