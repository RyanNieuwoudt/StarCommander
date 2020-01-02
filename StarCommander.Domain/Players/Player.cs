using System;

namespace StarCommander.Domain.Players
{
	public class Player : IAggregate
	{
		protected Player()
		{
		}

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

		public string CallSign { get; protected set; } = string.Empty;
		public string FirstName { get; protected set; } = string.Empty;
		public string LastName { get; protected set; } = string.Empty;
		public byte[] PasswordHash { get; protected set; } = new byte[0];
		public byte[] PasswordSalt { get; protected set; } = new byte[0];

		public Guid Id { get; }

		public static Player SignUp(Reference<Player> id, string callSign, string firstName, string lastName,
			byte[] passwordHash, byte[] passwordSalt)
		{
			return new Player(id, callSign, firstName, lastName, passwordHash, passwordSalt);
		}
	}
}