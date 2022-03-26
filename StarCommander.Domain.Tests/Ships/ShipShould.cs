using System;
using System.Linq;
using AutoFixture;
using NodaTime;
using NodaTime.Testing;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;
using Xunit;

namespace StarCommander.Domain.Tests.Ships;

public class ShipShould
{
	readonly IClock clock;
	readonly IFixture fixture;

	public ShipShould()
	{
		fixture = new Fixture();
		clock = new FakeClock(SystemClock.Instance.GetCurrentInstant());
	}

	[Fact]
	public void LaunchWithCorrectValues()
	{
		var id = fixture.Create<Reference<Ship>>();
		var player = fixture.Create<Reference<Player>>();

		var ship = Ship.Launch(clock, id, player);

		Assert.Equal(clock.GetCurrentInstant(), ship.NavigationComputer.Locate().Item1);
		Assert.Equal(ship.Reference, id);
		Assert.Equal(ship.Captain, player);
	}

	[Fact]
	public void RaiseEventOnLaunch()
	{
		var id = fixture.Create<Reference<Ship>>();
		var player = fixture.Create<Reference<Player>>();

		var ship = Ship.Launch(clock, id, player);

		var shipLaunched = ship.Events.Single(e => e is ShipLaunched) as ShipLaunched;

		Assert.NotNull(shipLaunched);
		Assert.Equal(ship.Reference, shipLaunched!.Ship);
		Assert.Equal(ship.Captain, shipLaunched!.Player);
	}
}