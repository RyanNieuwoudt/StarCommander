using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using StarCommander.Application.Services;
using StarCommander.Domain;
using static StarCommander.Shared.Model.Notifications.Action;

namespace StarCommander.Application.DomainEventHandlers
{
	public class NotifyPlayer : IWhen<INotifyPlayer>
	{
		readonly IChannelService channelService;
		readonly IMapper mapper;

		public NotifyPlayer(IChannelService channelService, IMapper mapper)
		{
			this.channelService = channelService;
			this.mapper = mapper;
		}

		public async Task Handle(INotifyPlayer @event, CancellationToken cancellationToken)
		{
			var sourceType = @event.GetType();
			var destinationType = NotificationRegistry.Player[sourceType];
			await channelService.MessagePlayer(@event.Player,
				WithPayload(mapper.Map(@event, sourceType, destinationType)));
		}
	}
}