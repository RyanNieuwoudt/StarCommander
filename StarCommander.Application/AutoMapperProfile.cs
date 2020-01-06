using System;
using System.Collections.Generic;
using AutoMapper;
using StarCommander.Domain.Players;

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
			foreach (var (sourceType, destinationType) in types)
			{
				CreateMap(sourceType, destinationType);
			}
		}
	}
}