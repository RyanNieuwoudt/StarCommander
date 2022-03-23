using System.Collections.Generic;
using System.Threading.Tasks;

namespace StarCommander.Domain;

public interface IRepository<T> where T : class, IAggregate
{
	Task<ICollection<T>> All();
	Task<T> Fetch(Reference<T> reference);
	Task Save(T aggregate);
	Task SaveAll(ICollection<T> aggregates);
	Task Remove(T aggregate);
	Task RemoveAll(ICollection<T> aggregates);
}