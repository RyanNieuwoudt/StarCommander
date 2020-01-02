using AutoMapper;

namespace StarCommander.Application
{
	public class AutoMapperProfile : Profile
	{
		public AutoMapperProfile()
		{
			CreateMap<Domain.Players.Player, Shared.Model.Player>();
		}
	}
}