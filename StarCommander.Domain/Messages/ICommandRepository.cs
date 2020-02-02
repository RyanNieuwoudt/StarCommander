using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarCommander.Domain.Messages
{
	public interface ICommandRepository : IRepository<Command>
	{
		Task<Command?> FetchNextUnprocessed();
		Task<IEnumerable<Command>> FetchForTarget(Guid targetId);
		Task<IEnumerable<Command>> FetchScheduledForTarget(Guid targetId);
	}
}