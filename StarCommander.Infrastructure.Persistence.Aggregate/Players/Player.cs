namespace StarCommander.Infrastructure.Persistence.Aggregate.Players
{
	public class Player : JsonEntity<Domain.Players.Player, PlayerJson>
	{
		public string CallSign { get; set; } = string.Empty;

		protected override void ProjectValues(Domain.Players.Player player)
		{
			CallSign = player.CallSign;
		}
	}
}