using System.Threading;
using System.Threading.Tasks;

namespace StarCommander.Domain;

public interface IHandleCommands<in T> where T : ICommand
{
	Task Handle(T command, CancellationToken cancellationToken);
}