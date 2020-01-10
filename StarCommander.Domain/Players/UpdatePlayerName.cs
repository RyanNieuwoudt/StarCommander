using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Players
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class UpdatePlayerName : PlayerCommand
	{
		public UpdatePlayerName(Reference<Player> player, string callSign, string firstName, string lastName) : base(
			player, callSign)
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