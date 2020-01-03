using System.Collections.Generic;

namespace StarCommander.Domain
{
	public interface IRaiseDomainEvents
	{
		IEnumerable<IDomainEvent> Events { get; }
		void ClearEvents();
	}
}