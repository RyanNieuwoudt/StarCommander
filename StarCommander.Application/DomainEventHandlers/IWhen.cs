using StarCommander.Domain;

namespace StarCommander.Application.DomainEventHandlers;

public interface IWhen<in T> : IHandleDomainEvents<T> where T : IDomainEvent
{
}