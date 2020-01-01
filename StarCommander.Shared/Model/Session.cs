namespace StarCommander.Shared.Model
{
	public class Session
	{
		public string Token { get; set; } = string.Empty;
		public Player Player { get; set; } = new Player();
	}
}