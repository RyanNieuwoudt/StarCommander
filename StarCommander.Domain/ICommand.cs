namespace StarCommander.Domain
{
	public interface ICommand
	{
		string Type { get; }
	}
}