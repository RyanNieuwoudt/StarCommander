using System.Threading;
using System.Threading.Tasks;

namespace StarCommander.Application.Events
{
	public interface IMessageForwarder
	{
		Task<bool> ForwardNextMessage(CancellationToken cancellationToken);
	}
}