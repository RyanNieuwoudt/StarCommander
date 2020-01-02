using System.Threading.Tasks;

namespace StarCommander.Shared.Communication
{
	public interface IChannelClient
	{
		Task Message(string message);
	}
}