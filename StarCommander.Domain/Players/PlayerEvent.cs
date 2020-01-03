namespace StarCommander.Domain.Players
{
	public abstract class PlayerEvent : DomainEvent
	{
		public PlayerEvent(string callSign)
		{
			CallSign = callSign;
		}

		public string CallSign { get; protected set; }
	}
}