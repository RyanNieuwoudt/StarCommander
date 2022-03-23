using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using StarCommander.Domain;
using StarCommander.Domain.Players;
using StarCommander.Shared.Communication;
using static StarCommander.Hubs.Channels;

namespace StarCommander.Hubs;

public class ChannelHub : Hub<IChannelClient>, IChannelServer
{
	public override async Task OnConnectedAsync()
	{
		await Groups.AddToGroupAsync(Context.ConnectionId, GetPlayerChannel(GetPlayerReference()));
		await base.OnConnectedAsync();
	}

	Reference<Player> GetPlayerReference() => Reference.To<Player>(new (GetUserClaim(ClaimTypes.Name)));

	string GetUserClaim(string type) => Context.User?.FindFirst(type)?.Value ?? string.Empty;
}