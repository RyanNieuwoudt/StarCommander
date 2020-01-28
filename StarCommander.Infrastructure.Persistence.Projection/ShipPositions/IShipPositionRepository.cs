using System.Collections.Generic;
using System.Threading.Tasks;
using StarCommander.Domain;
using StarCommander.Domain.Ships;

namespace StarCommander.Infrastructure.Persistence.Projection.ShipPositions
{
	public interface IShipPositionRepository : IProjectionRepository<ShipPosition>
	{
		Task<IEnumerable<ShipPosition>> Fetch(Reference<Ship> ship);
	}
}