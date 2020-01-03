using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Players
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class Player : EventPublisherBase, IAggregate
	{
		[JsonConstructor]
		Player(Reference<Player> id, string callSign, string firstName, string lastName, byte[] passwordHash,
			byte[] passwordSalt)
		{
			Id = id;
			CallSign = callSign;
			FirstName = firstName;
			LastName = lastName;
			PasswordHash = passwordHash;
			PasswordSalt = passwordSalt;
		}

		[JsonProperty]
		public string CallSign { get; private set; }

		[JsonProperty]
		public string FirstName { get; private set; }

		[JsonProperty]
		public string LastName { get; private set; }

		[JsonProperty]
		public byte[] PasswordHash { get; private set; }

		[JsonProperty]
		public byte[] PasswordSalt { get; private set; }

		[JsonProperty]
		public Guid Id { get; }

		public static Player SignUp(Reference<Player> id, string callSign, string firstName, string lastName,
			byte[] passwordHash, byte[] passwordSalt)
		{
			return new Player(id, callSign, firstName, lastName, passwordHash, passwordSalt);
		}

		public void UpdateName(string firstName, string lastName)
		{
			if (firstName == FirstName && lastName == LastName)
			{
				return;
			}

			FirstName = firstName;
			LastName = lastName;

			RaiseEvent(new PlayerNameChanged(CallSign, FirstName, LastName));
		}
	}
}