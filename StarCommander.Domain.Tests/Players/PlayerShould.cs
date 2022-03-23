using System;
using System.Linq;
using AutoFixture;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;
using Xunit;

namespace StarCommander.Domain.Tests.Players;

public class PlayerShould
{
	readonly IFixture fixture;

	public PlayerShould() => fixture = new Fixture();

	[Fact]
	public void RaiseEventOnSignIn()
	{
		var id = fixture.Create<Reference<Player>>();
		var callSign = fixture.Create<string>();
		var firstName = fixture.Create<string>();
		var lastName = fixture.Create<string>();

		var player = Player.SignUp(id, callSign, firstName, lastName, Array.Empty<byte>(), Array.Empty<byte>());

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

		var player = Player.SignUp(id, callSign, firstName, lastName, Array.Empty<byte>(), Array.Empty<byte>());

		var playerSignedUp = player.Events.Single(e => e is PlayerSignedUp) as PlayerSignedUp;

		Assert.NotNull(playerSignedUp);
		Assert.Equal(player.Reference, playerSignedUp!.Player);
	}

	[Fact]
	public void RaiseEventWhenAssigningShip()
	{
		var id = fixture.Create<Reference<Player>>();
		var callSign = fixture.Create<string>();
		var firstName = fixture.Create<string>();
		var lastName = fixture.Create<string>();

		var player = Player.SignUp(id, callSign, firstName, lastName, Array.Empty<byte>(), Array.Empty<byte>());

		var shipId = fixture.Create<Reference<Ship>>();

		player.AssignShip(shipId);

		Assert.Equal(shipId, player.Ship);

		var shipAssigned = player.Events.Single(e => e is ShipAssigned) as ShipAssigned;

		Assert.NotNull(shipAssigned);
		Assert.Equal(player.Reference, shipAssigned!.Player);
		Assert.Equal(player.Ship, shipAssigned.Ship);
	}

	[Fact]
	public void RaiseEventWhenBoardingShip()
	{
		var id = fixture.Create<Reference<Player>>();
		var callSign = fixture.Create<string>();
		var firstName = fixture.Create<string>();
		var lastName = fixture.Create<string>();

		var player = Player.SignUp(id, callSign, firstName, lastName, Array.Empty<byte>(), Array.Empty<byte>());

		player.BoardShip();

		var captainBoarded = player.Events.Single(e => e is CaptainBoarded) as CaptainBoarded;

		Assert.NotNull(captainBoarded);
		Assert.Equal(player.Reference, captainBoarded!.Player);
	}

	[Fact]
	public void SignUpWithCorrectValues()
	{
		var id = fixture.Create<Reference<Player>>();
		var callSign = fixture.Create<string>();
		var firstName = fixture.Create<string>();
		var lastName = fixture.Create<string>();

		var player = Player.SignUp(id, callSign, firstName, lastName, Array.Empty<byte>(), Array.Empty<byte>());

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
			lastName, Array.Empty<byte>(), Array.Empty<byte>());

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