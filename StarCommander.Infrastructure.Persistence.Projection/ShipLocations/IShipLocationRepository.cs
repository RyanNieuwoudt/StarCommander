using System.Collections.Generic;
using System.Threading.Tasks;
using StarCommander.Domain;
using StarCommander.Domain.Ships;

namespace StarCommander.Infrastructure.Persistence.Projection.ShipLocations
{
	public interface IShipLocationRepository : IProjectionRepository<ShipLocation>
	{
		Task<IEnumerable<ShipLocation>> Fetch(Reference<Ship> ship);
	}
}