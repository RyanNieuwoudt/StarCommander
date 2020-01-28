using System.Threading.Tasks;
using StarCommander.Domain;
using StarCommander.Domain.Ships;

namespace StarCommander.Application.Projectors
{
	public interface IShipLocationProjector
	{
		Task Project(Reference<Ship> ship, Position position);
	}
}