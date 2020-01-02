using System.Threading.Tasks;

namespace StarCommander.Domain.Players
{
	public interface IPlayerRepository : IRepository<Player>
	{
		Task<bool> Exists(string callSign);
	}
}