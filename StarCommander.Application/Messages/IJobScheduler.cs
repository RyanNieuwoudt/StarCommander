using System.Threading;
using System.Threading.Tasks;
using StarCommander.Domain.Messages;

namespace StarCommander.Application.Messages
{
	public interface IJobScheduler
	{
		void Add(Job job);
		Task Start(CancellationToken cancellationToken, bool autoStop = false);
		Task Stop();
	}
}