using System.Threading.Tasks;

namespace StarCommander.Application.Services
{
	public interface IChannelService
	{
		Task MessagePlayer(string callSign, string message);
	}
}