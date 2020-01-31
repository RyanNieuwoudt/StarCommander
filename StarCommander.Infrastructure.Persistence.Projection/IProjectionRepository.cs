using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarCommander.Infrastructure.Persistence.Projection
{
	public interface IProjectionRepository<T> where T : IProjectWithKey<T>
	{
		Task Delete(List<T> projections);
		Task Insert(List<T> projections);
		Task Update(List<T> projections);
	}
}