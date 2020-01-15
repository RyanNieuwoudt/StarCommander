using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Ships
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class SetHeading : ShipCommand
	{
		[JsonConstructor]
		public SetHeading(Reference<Ship> ship, Heading heading) : base(ship)
		{
			Heading = heading;
		}

		[JsonProperty]
		public Heading Heading { get; private set; }
	}
}