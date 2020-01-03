namespace StarCommander.Domain.Players
{
	public class PlayerNameChanged : PlayerEvent, INotifyPlayer
	{
		public PlayerNameChanged(string callSign, string firstName, string lastName) : base(callSign)
		{
			FirstName = firstName;
			LastName = lastName;
		}

		public string FirstName { get; protected set; }
		public string LastName { get; protected set; }
	}
}