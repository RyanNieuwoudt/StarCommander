using System.Threading.Tasks;
using StarCommander.Domain;
using StarCommander.Domain.Players;

namespace StarCommander.Application.Services;

public interface IChannelService
{
	Task MessagePlayer(Reference<Player> player, string message);
}