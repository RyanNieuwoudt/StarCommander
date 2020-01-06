using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using StarCommander.Shared.Communication;
using static StarCommander.Hubs.Channels;

namespace StarCommander.Hubs
{
	public class ChannelHub : Hub<IChannelClient>, IChannelServer
	{
		public override async Task OnConnectedAsync()
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, GetPlayerChannel(GetCallSign()));

			await base.OnConnectedAsync();
		}

		string GetCallSign()
		{
			return GetUserClaim(ClaimTypes.Name);
		}

		string GetUserClaim(string type)
		{
			return Context.User.FindFirst(type)?.Value ?? string.Empty;
		}
	}
}