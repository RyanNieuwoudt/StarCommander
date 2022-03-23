using System;
using System.Security;
using System.Threading.Tasks;
using AutoFixture;
using Microsoft.Extensions.DependencyInjection;
using StarCommander.Application.Services;
using Xunit;

namespace StarCommander.Application.Tests.Services;

public class PlayerServiceShould : IClassFixture<ServicesFixture>
{
	readonly IFixture fixture;
	readonly ServicesFixture servicesFixture;

	public PlayerServiceShould(ServicesFixture servicesFixture)
	{
		fixture = new Fixture();
		this.servicesFixture = servicesFixture;
	}

	[Fact]
	public async Task PreventSignInForIncorrectPassword()
	{
		using var scope = servicesFixture.ServiceProvider.CreateScope();
		var playerService = scope.ServiceProvider.GetService<IPlayerService>()!;

		var callSign = fixture.Create<string>();

		await playerService.SignUp(callSign, fixture.Create<string>(), fixture.Create<string>(),
			fixture.Create<string>());

		await Assert.ThrowsAsync<SecurityException>(async () =>
			await playerService.SignIn(callSign, fixture.Create<string>()));
	}

	[Fact]
	public async Task PreventSignInForUnknownCallSign()
	{
		using var scope = servicesFixture.ServiceProvider.CreateScope();
		var playerService = scope.ServiceProvider.GetService<IPlayerService>()!;

		var callSign = fixture.Create<string>();
		var password = fixture.Create<string>();

		await Assert.ThrowsAsync<SecurityException>(async () => await playerService.SignIn(callSign, password));
	}

	[Fact]
	public async Task PreventSignUpWhenCallSignExists()
	{
		using var scope = servicesFixture.ServiceProvider.CreateScope();
		var playerService = scope.ServiceProvider.GetService<IPlayerService>()!;

		var callSign = fixture.Create<string>();

		await playerService.SignUp(callSign, fixture.Create<string>(), fixture.Create<string>(),
			fixture.Create<string>());

		await Assert.ThrowsAsync<InvalidOperationException>(async () =>
			await playerService.SignUp(callSign, fixture.Create<string>(), fixture.Create<string>(),
				fixture.Create<string>()));
	}

	[Fact]
	public async Task SignInWithPassword()
	{
		using var scope = servicesFixture.ServiceProvider.CreateScope();
		var playerService = scope.ServiceProvider.GetService<IPlayerService>()!;

		var callSign = fixture.Create<string>();
		var firstName = fixture.Create<string>();
		var lastName = fixture.Create<string>();
		var password = fixture.Create<string>();

		await playerService.SignUp(callSign, firstName, lastName, password);

		var session = await playerService.SignIn(callSign, password);

		Assert.False(string.IsNullOrWhiteSpace(session.Token));
		Assert.Equal(callSign, session.Player.CallSign);
		Assert.Equal(firstName, session.Player.FirstName);
		Assert.Equal(lastName, session.Player.LastName);
	}

	[Fact]
	public async Task SignUpNewPlayer()
	{
		using var scope = servicesFixture.ServiceProvider.CreateScope();
		var playerService = scope.ServiceProvider.GetService<IPlayerService>()!;

		var callSign = fixture.Create<string>();
		var firstName = fixture.Create<string>();
		var lastName = fixture.Create<string>();

		var session = await playerService.SignUp(callSign, firstName, lastName, fixture.Create<string>())!;

		Assert.False(string.IsNullOrWhiteSpace(session.Token));
		Assert.Equal(callSign, session.Player.CallSign);
		Assert.Equal(firstName, session.Player.FirstName);
		Assert.Equal(lastName, session.Player.LastName);
	}
}