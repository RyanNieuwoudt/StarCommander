using System;

namespace StarCommander.Domain.Players
{
	public class Player : IAggregate
	{
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

		public string CallSign { get; }
		public string FirstName { get; }
		public string LastName { get; }
		public byte[] PasswordHash { get; }
		public byte[] PasswordSalt { get; }

		public Guid Id { get; }

		public static Player SignUp(Reference<Player> id, string callSign, string firstName, string lastName,
			byte[] passwordHash, byte[] passwordSalt)
		{
			return new Player(id, callSign, firstName, lastName, passwordHash, passwordSalt);
		}
	}
}