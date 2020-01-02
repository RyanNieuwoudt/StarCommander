using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using StarCommander.Application.Services;
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

		public async Task MessagePlayer(string callSign, string message)
		{
			await channelHubContext.Clients.Group(GetPlayerChannel(callSign)).Message(message);
		}
	}
}