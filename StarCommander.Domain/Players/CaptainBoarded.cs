using System;
using Newtonsoft.Json;
using StarCommander.Domain.Ships;

namespace StarCommander.Domain.Players
{
	[Serializable]
	[JsonObject(MemberSerialization.OptIn)]
	public class CaptainBoarded : PlayerEvent, INotifyPlayer
	{
		[JsonConstructor]
		public CaptainBoarded(Reference<Player> player, Reference<Ship> ship) : base(player)
		{
		}
	}
}