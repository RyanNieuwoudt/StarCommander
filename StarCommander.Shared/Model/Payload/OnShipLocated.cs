namespace StarCommander.Shared.Model.Payload
{
	public class OnShipLocated : OnShip
	{
		public double Heading { get; set; }
		public Position Position { get; set; } = new Position();
		public long Speed { get; set; }
	}
}