using System.Collections.Generic;
using System.Threading.Tasks;
using StarCommander.Domain;
using StarCommander.Domain.Ships;
using StarCommander.Shared.Model.Query;

namespace StarCommander.Application.Queries
{
	public interface IShipQuery
	{
		Task<IEnumerable<ScanResult>> ScanForNearbyShips(Reference<Ship> ship);
	}
}