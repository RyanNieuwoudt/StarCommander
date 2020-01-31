using System.Threading.Tasks;

namespace StarCommander.Domain.Messages
{
	public interface IEventRepository : IRepository<Event>
	{
		Task<Event?> FetchNextUnprocessed();
	}
}