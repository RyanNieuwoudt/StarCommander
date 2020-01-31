using System.Threading;
using System.Threading.Tasks;
using StarCommander.Application.Services;
using StarCommander.Domain.Players;

namespace StarCommander.Application.CommandHandlers
{
	public class PlayerCommandHandler : IObey<UpdatePlayerName>
	{
		readonly IPlayerService playerService;

		public PlayerCommandHandler(IPlayerService playerService)
		{
			this.playerService = playerService;
		}

		public async Task Handle(UpdatePlayerName command, CancellationToken cancellationToken)
		{
			await playerService.UpdateName(command.Player, command.FirstName, command.LastName);
		}
	}
}