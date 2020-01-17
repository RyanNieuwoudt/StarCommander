using System.Threading.Tasks;

namespace StarCommander.Domain.Messages
{
	public interface ICommandRepository : IRepository<Command>
	{
		Task<Command?> FetchNextUnprocessed();
	}
}