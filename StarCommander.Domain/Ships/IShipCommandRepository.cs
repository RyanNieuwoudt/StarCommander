using System.Threading.Tasks;
using StarCommander.Domain.Messages;

namespace StarCommander.Domain.Ships
{
	public interface IShipCommandRepository : ICommandRepository
	{
		public Task<Ship> Fetch(Reference<Ship> reference);
	}
}