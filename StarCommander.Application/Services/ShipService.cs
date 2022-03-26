using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using NodaTime;
using StarCommander.Domain;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;

namespace StarCommander.Application.Services;

public class ShipService : IShipService
{
	readonly IClock clock;
	readonly IDbContextScopeFactory dbContextScopeFactory;
	readonly IShipRepository shipRepository;

	public ShipService(IClock clock, IDbContextScopeFactory dbContextScopeFactory, IShipRepository shipRepository)
	{
		this.clock = clock;
		this.dbContextScopeFactory = dbContextScopeFactory;
		this.shipRepository = shipRepository;
	}

	public async Task Launch(Reference<Ship> ship, Reference<Player> player)
	{
		using var dbContextScope = dbContextScopeFactory.Create();

		var s = Ship.Launch(clock, ship, player);

		await shipRepository.Save(s);
		await dbContextScope.SaveChangesAsync();
	}

	public async Task Locate(Reference<Ship> ship)
	{
		using var dbContextScope = dbContextScopeFactory.Create();

		var s = await shipRepository.Fetch(ship);
		s.Locate();

		await shipRepository.Save(s);
		await dbContextScope.SaveChangesAsync();
	}
}