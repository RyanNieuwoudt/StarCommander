using System;
using System.Collections.Generic;
using AutoMapper;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;

namespace StarCommander.Application
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Player, Shared.Model.Player>();

			MapNotifications(NotificationRegistry.Player);
		}

		void MapNotifications(IReadOnlyDictionary<Type, Type> types)
		{
			CreateMap<Position, Shared.Model.Payload.Position>();

			foreach (var (sourceType, destinationType) in types)
			{
				CreateMap(sourceType, destinationType);
			}
		}
	}
}