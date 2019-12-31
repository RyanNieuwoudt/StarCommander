using System;

namespace StarCommander.Domain.Players
{
	public class Player : IAggregate
	{
		Player(Reference<Player> id, string callSign, string firstName, string lastName)
		{
			Id = id;
			CallSign = callSign;
			FirstName = firstName;
			LastName = lastName;
		}

		public string CallSign { get; }
		public string FirstName { get; }
		public string LastName { get; }

		public Guid Id { get; }

		public static Player Create(Reference<Player> id, string callSign, string firstName, string lastName)
		{
			return new Player(id, callSign, firstName, lastName);
		}
	}
}