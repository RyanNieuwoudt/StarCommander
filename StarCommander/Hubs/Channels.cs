using StarCommander.Domain;
using StarCommander.Domain.Players;

namespace StarCommander.Hubs;

static class Channels
{
	internal static string GetPlayerChannel(in Reference<Player> player)
	{
		return $"{player.Id.ToString().ToLower()}";
	}
}