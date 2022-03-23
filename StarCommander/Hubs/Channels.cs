using StarCommander.Domain;
using StarCommander.Domain.Players;

namespace StarCommander.Hubs;

static class Channels
{
	internal static string GetPlayerChannel(in Reference<Player> player) => $"{player.Id.ToString().ToLower()}";
}