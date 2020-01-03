namespace StarCommander.Domain
{
	public abstract class DomainEvent : IDomainEvent
	{
		public string Type => GetType().FullName;
	}
}