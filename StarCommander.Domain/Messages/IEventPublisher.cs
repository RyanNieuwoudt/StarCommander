using System.Threading.Tasks;

namespace StarCommander.Domain.Messages;

public interface IEventPublisher
{
	Task Publish(params IRaiseDomainEvents[] aggregates);
}