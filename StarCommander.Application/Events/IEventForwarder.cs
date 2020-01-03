using System.Threading;
using System.Threading.Tasks;

namespace StarCommander.Application.Events
{
	public interface IEventForwarder
	{
		Task<bool> ForwardNextEvent(CancellationToken cancellationToken);
	}
}