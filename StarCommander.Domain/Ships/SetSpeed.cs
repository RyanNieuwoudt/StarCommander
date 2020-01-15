using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Ships
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class SetSpeed : ShipCommand
	{
		[JsonConstructor]
		public SetSpeed(Reference<Ship> ship, Speed speed) : base(ship)
		{
			Speed = speed;
		}

		[JsonProperty]
		public Speed Speed { get; private set; }
	}
}