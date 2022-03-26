using System.Threading.Tasks;
using NodaTime;
using StarCommander.Domain;
using StarCommander.Domain.Ships;

namespace StarCommander.Application.Projectors;

public interface IShipLocationProjector
{
	Task Project(Reference<Ship> ship, Instant instant, Heading heading, Position position, Speed speed);
}