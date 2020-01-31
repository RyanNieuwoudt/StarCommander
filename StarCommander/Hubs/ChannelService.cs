using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using StarCommander.Application.Services;
using StarCommander.Domain;
using StarCommander.Domain.Players;
using StarCommander.Shared.Communication;
using static StarCommander.Hubs.Channels;

namespace StarCommander.Hubs
{
	public class ChannelService : IChannelService
	{
		readonly IHubContext<ChannelHub, IChannelClient> channelHubContext;

		public ChannelService(IHubContext<ChannelHub, IChannelClient> channelHubContext)
		{
			this.channelHubContext = channelHubContext;
		}

		public async Task MessagePlayer(Reference<Player> player, string message)
		{
			await channelHubContext.Clients.Group(GetPlayerChannel(player)).Message(message);
		}
	}
}