using StarCommander.Domain;

namespace StarCommander.Application.DomainEventHandlers
{
	public interface IWhen
	{
	}

	public interface IWhen<in T> : IWhen, IHandleDomainEvents<T> where T : IDomainEvent
	{
	}
}