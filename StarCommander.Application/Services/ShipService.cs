using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using StarCommander.Domain;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;

namespace StarCommander.Application.Services
{
	public class ShipService : IShipService
	{
		readonly IDbContextScopeFactory dbContextScopeFactory;
		readonly IReferenceGenerator generator;
		readonly IShipRepository shipRepository;

		public ShipService(IDbContextScopeFactory dbContextScopeFactory, IReferenceGenerator generator,
			IShipRepository shipRepository)
		{
			this.dbContextScopeFactory = dbContextScopeFactory;
			this.generator = generator;
			this.shipRepository = shipRepository;
		}

		public async Task Launch(Reference<Player> player)
		{
			using var dbContextScope = dbContextScopeFactory.Create();

			var ship = Ship.Launch(generator.NewReference<Ship>(), player);

			await shipRepository.Save(ship);
			await dbContextScope.SaveChangesAsync();
		}
	}
}