using System.Threading.Tasks;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;

namespace StarCommander.Application.Services
{
	public interface ICommandService
	{
		Task Issue(PlayerCommand command);
		Task Issue(ShipCommand command);
	}
}