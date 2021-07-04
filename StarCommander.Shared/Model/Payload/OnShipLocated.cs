namespace StarCommander.Shared.Model.Payload
{
	public class OnShipLocated : OnShip
	{
		public double Heading { get; set; }
		public Position Position { get; set; } = new ();
		public long Speed { get; set; }
	}
}