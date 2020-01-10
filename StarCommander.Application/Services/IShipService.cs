using System.Threading.Tasks;
using StarCommander.Domain;
using StarCommander.Domain.Players;

namespace StarCommander.Application.Services
{
	public interface IShipService
	{
		Task Launch(Reference<Player> player);
	}
}