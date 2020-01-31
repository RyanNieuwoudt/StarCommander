using System.Threading.Tasks;
using StarCommander.Domain;
using StarCommander.Domain.Ships;
using StarCommander.Shared.Model;
using Player = StarCommander.Domain.Players.Player;

namespace StarCommander.Application.Services
{
	public interface IPlayerService
	{
		Task AssignShip(Reference<Player> player, Reference<Ship> ship);
		Task BoardShip(Reference<Player> player);
		Task<Session> SignIn(string callSign, string password);
		Task<Session> SignUp(string callSign, string firstName, string lastName, string password);
		Task UpdateName(Reference<Player> player, string firstName, string lastName);
	}
}