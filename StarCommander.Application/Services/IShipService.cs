using System.Threading.Tasks;
using StarCommander.Domain;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;

namespace StarCommander.Application.Services
{
	public interface IShipService
	{
		Task Launch(Reference<Player> player);
		Task Locate(Reference<Ship> ship);
	}
}