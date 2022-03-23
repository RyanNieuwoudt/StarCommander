using System.Collections.Generic;
using System.Threading.Tasks;
using StarCommander.Domain;
using StarCommander.Domain.Ships;
using StarCommander.Shared.Model.Query;

namespace StarCommander.Infrastructure.Persistence.Projection.ShipLocations;

public interface IQueryShipLocations
{
	Task<IEnumerable<ScanResult>> ScanForNearbyShips(Reference<Ship> ship);
}