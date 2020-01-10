using System;
using Newtonsoft.Json;

namespace StarCommander.Domain.Ships
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public abstract class ShipCommand : Command
	{
		[JsonConstructor]
		protected ShipCommand(Reference<Ship> ship)
		{
			Ship = ship;
		}

		[JsonProperty]
		public Reference<Ship> Ship { get; private set; }
	}
}