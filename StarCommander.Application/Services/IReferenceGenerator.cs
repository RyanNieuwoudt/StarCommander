using StarCommander.Domain;

namespace StarCommander.Application.Services
{
	public interface IReferenceGenerator
	{
		Reference<T> Reference<T>() where T : IAggregate;
	}
}