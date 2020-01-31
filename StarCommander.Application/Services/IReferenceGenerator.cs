using StarCommander.Domain;

namespace StarCommander.Application.Services
{
	public interface IReferenceGenerator
	{
		Reference<T> NewReference<T>() where T : IAggregate;
	}
}