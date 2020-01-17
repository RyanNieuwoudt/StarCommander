using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Ships
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class LocateShip : ShipCommand
	{
		[JsonConstructor]
		public LocateShip(Reference<Ship> ship) : base(ship)
		{
		}
	}
}