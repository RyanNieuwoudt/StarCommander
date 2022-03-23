using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using StarCommander.Domain.Messages;

namespace StarCommander.Application.Services;

public interface IJobService
{
	Task<ICollection<Job>> Fetch();
	Task Run(Job job, CancellationToken cancellationToken);
}