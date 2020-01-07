using StarCommander.Domain;

namespace StarCommander.Application.CommandHandlers
{
	public interface IObey
	{
	}

	public interface IObey<in T> : IObey, IHandleCommands<T> where T : ICommand
	{
	}
}