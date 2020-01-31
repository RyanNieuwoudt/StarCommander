using StarCommander.Domain;

namespace StarCommander.Application.CommandHandlers
{
	public interface IObey<in T> : IHandleCommands<T> where T : ICommand
	{
	}
}