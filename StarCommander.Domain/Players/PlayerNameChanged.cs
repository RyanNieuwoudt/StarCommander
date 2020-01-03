using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Players
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class PlayerNameChanged : PlayerEvent, INotifyPlayer
	{
		[JsonConstructor]
		public PlayerNameChanged(string callSign, string firstName, string lastName) : base(callSign)
		{
			FirstName = firstName;
			LastName = lastName;
		}

		[JsonProperty]
		public string FirstName { get; private set; }

		[JsonProperty]
		public string LastName { get; private set; }
	}
}