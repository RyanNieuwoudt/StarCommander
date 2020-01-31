using System.Collections.Generic;

namespace StarCommander.Domain
{
	public abstract class EventPublisherBase : IRaiseDomainEvents
	{
		readonly List<IDomainEvent> events;
		bool enableEventPublishing;

		protected EventPublisherBase() : this(true)
		{
		}

		protected EventPublisherBase(bool enableEventPublishing)
		{
			this.enableEventPublishing = enableEventPublishing;
			events = new List<IDomainEvent>();
		}

		public IEnumerable<IDomainEvent> Events => events;

		public void ClearEvents()
		{
			events.Clear();
		}

		protected void EnableEventPublishing()
		{
			enableEventPublishing = true;
		}

		internal void RaiseEvent(IDomainEvent @event)
		{
			if (enableEventPublishing)
			{
				events.Add(@event);
			}
		}
	}
}