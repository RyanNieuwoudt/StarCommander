using System.Collections.Generic;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using StarCommander.Domain;
using StarCommander.Domain.Ships;
using StarCommander.Infrastructure.Persistence.Projection.ShipLocations;
using StarCommander.Shared.Model.Query;

namespace StarCommander.Application.Queries
{
	public class ShipQuery : IShipQuery
	{
		readonly IDbContextScopeFactory dbContextScopeFactory;
		readonly IQueryShipLocations queryShipLocations;

		public ShipQuery(IDbContextScopeFactory dbContextScopeFactory, IQueryShipLocations queryShipLocations)
		{
			this.dbContextScopeFactory = dbContextScopeFactory;
			this.queryShipLocations = queryShipLocations;
		}

		public async Task<IEnumerable<ScanResult>> ScanForNearbyShips(Reference<Ship> ship)
		{
			using (dbContextScopeFactory.CreateReadOnly())
			{
				return await queryShipLocations.ScanForNearbyShips(ship);
			}
		}
	}
}