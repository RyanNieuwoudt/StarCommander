using System.Threading;
using System.Threading.Tasks;

namespace StarCommander.Domain;

public interface IHandleDomainEvents<in T> where T : IDomainEvent
{
	Task Handle(T @event, CancellationToken cancellationToken);
}