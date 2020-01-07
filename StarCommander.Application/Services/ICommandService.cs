using System.Threading.Tasks;
using StarCommander.Domain.Players;

namespace StarCommander.Application.Services
{
	public interface ICommandService
	{
		Task Issue(PlayerCommand command);
	}
}