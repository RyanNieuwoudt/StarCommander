using System.Collections.Generic;
using StarCommander.Domain;

namespace StarCommander.Application.Events
{
	public interface IWorkerRegistry
	{
		IEnumerable<string> GetHandlersFor(IDomainEvent @event);
	}
}