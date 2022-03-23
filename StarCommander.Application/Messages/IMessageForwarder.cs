using System.Threading;
using System.Threading.Tasks;

namespace StarCommander.Application.Messages;

public interface IMessageForwarder
{
	Task<bool> ForwardNextMessage(CancellationToken cancellationToken);
}