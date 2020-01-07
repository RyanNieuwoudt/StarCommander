using System.Threading.Tasks;

namespace StarCommander.Domain.Messages
{
	public interface ICommandRepository<T> : IRepository<Command<T>> where T : notnull, IAggregate
	{
		Task<Event?> FetchNextUnprocessed();
	}
}