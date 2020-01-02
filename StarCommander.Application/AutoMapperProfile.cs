using AutoMapper;
using StarCommander.Domain.Players;

namespace StarCommander.Application
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Player, Shared.Model.Player>();
		}
	}
}