namespace StarCommander.Domain
{
	public interface IDomainEvent
	{
		string Type { get; }
	}
}