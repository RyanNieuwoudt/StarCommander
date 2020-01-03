namespace StarCommander.Domain
{
	public interface INotifyPlayer : IDomainEvent
	{
		public string CallSign { get; }
	}
}