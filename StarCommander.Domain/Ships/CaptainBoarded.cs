using System;
using Newtonsoft.Json;
using StarCommander.Domain.Players;

namespace StarCommander.Domain.Ships
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class CaptainBoarded : ShipEvent, INotifyPlayer
	{
		[JsonConstructor]
		public CaptainBoarded(Reference<Ship> ship, Reference<Player> player) : base(ship, player)
		{
		}
	}
}