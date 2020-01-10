using System.Threading.Tasks;
using StarCommander.Domain;
using StarCommander.Shared.Model;

namespace StarCommander.Application.Services
{
	public interface IPlayerService
	{
		Task<Session> SignIn(string callSign, string password);
		Task<Session> SignUp(string callSign, string firstName, string lastName, string password);
		Task UpdateName(Reference<Domain.Players.Player> player, string firstName, string lastName);
	}
}