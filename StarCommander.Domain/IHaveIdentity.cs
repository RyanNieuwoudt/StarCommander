namespace StarCommander.Domain
{
	public interface IHaveIdentity<out T>
	{
		T Id { get; }
	}
}