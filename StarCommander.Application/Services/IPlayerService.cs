using System.Threading.Tasks;
using StarCommander.Shared.Model;

namespace StarCommander.Application.Services
{
	public interface IPlayerService
	{
		Task<Session> SignIn(string callSign, string password);
		Task<Session> SignUp(string callSign, string firstName, string lastName, string password);
		Task UpdateName(string callSign, string firstName, string lastName);
	}
}