using System;
using System.Collections.Generic;
using AutoMapper;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;
using StarCommander.Infrastructure.Persistence.Projection;
using static StarCommander.Application.Reflection;

namespace StarCommander.Application
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Player, Shared.Model.Player>();

			MapNotifications(NotificationRegistry.Player);
			MapProjections();
		}

		void MapNotifications(IReadOnlyDictionary<Type, Type> types)
		{
			CreateMap<Position, Shared.Model.Payload.Position>();

			foreach (var (sourceType, destinationType) in types)
			{
				CreateMap(sourceType, destinationType);
			}
		}

		void MapProjections()
		{
			var openGenericType = typeof(ProjectWithKeyBase<>);
			foreach (var type in GetAllTypesImplementingOpenGenericType(openGenericType, openGenericType.Assembly))
			{
				CreateMap(type, type);
			}
		}
	}
}