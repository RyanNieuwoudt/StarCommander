namespace StarCommander.Hubs
{
	static class Channels
	{
		internal static string GetPlayerChannel(string callSign)
		{
			return $"Player-{callSign}";
		}
	}
}