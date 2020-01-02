namespace StarCommander.Infrastructure.Persistence.Aggregate.Players
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class PlayerJson : Domain.Players.Player
	{
		public new string CallSign
		{
			get => base.CallSign;
			set => base.CallSign = value;
		}

		public new string FirstName
		{
			get => base.FirstName;
			set => base.FirstName = value;
		}

		public new string LastName
		{
			get => base.LastName;
			set => base.LastName = value;
		}

		public new byte[] PasswordHash
		{
			get => base.PasswordHash;
			set => base.PasswordHash = value;
		}

		public new byte[] PasswordSalt
		{
			get => base.PasswordSalt;
			set => base.PasswordSalt = value;
		}
	}
}