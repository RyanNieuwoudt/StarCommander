using StarCommander.Domain.Players;

namespace StarCommander.Domain;

public interface INotifyPlayer : IDomainEvent
{
	Reference<Player> Player { get; }
}