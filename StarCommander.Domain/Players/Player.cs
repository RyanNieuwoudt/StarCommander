using System;
using Newtonsoft.Json;
using StarCommander.Domain.Ships;
using static StarCommander.Domain.Reference;

namespace StarCommander.Domain.Players
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class Player : EventPublisherBase, IAggregate
	{
		[JsonConstructor]
		Player(Reference<Player> id, string callSign, string firstName, string lastName, byte[] passwordHash,
			byte[] passwordSalt, Reference<Ship> ship)
		{
			Id = id;
			CallSign = callSign;
			FirstName = firstName;
			LastName = lastName;
			PasswordHash = passwordHash;
			PasswordSalt = passwordSalt;
			Ship = ship;
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

		public Reference<Player> Reference => To(this);

		[JsonProperty]
		public Reference<Ship> Ship { get; private set; }

		[JsonProperty]
		public Guid Id { get; }

		public static Player SignUp(Reference<Player> id, string callSign, string firstName, string lastName,
			byte[] passwordHash, byte[] passwordSalt)
		{
			var player = new Player(id, callSign, firstName, lastName, passwordHash, passwordSalt,
				Reference<Ship>.None);
			player.RaiseEvent(new PlayerSignedUp(player.Reference));
			return player;
		}

		public void SignIn()
		{
			RaiseEvent(new PlayerSignedIn(Reference));
		}

		public void UpdateName(string firstName, string lastName)
		{
			if (firstName == FirstName && lastName == LastName)
			{
				return;
			}

			FirstName = firstName;
			LastName = lastName;

			RaiseEvent(new PlayerNameChanged(Reference, FirstName, LastName));
		}
	}
}