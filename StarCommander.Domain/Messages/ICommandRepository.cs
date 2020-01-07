using System.Threading.Tasks;

namespace StarCommander.Domain.Messages
{
	public interface ICommandRepository<T> : IRepository<Command>
	{
		Task<Command?> FetchNextUnprocessed();
	}
}