using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using Microsoft.EntityFrameworkCore;
using StarCommander.Domain;

namespace StarCommander.Infrastructure.Persistence
{
	public abstract class JsonRepositoryBase<T, T1, TU, TV> : RepositoryBase<TV>, IRepository<T>
		where T : class, IAggregate
		where T1 : class, T
		where TU : JsonEntity<T, T1>
		where TV : class, IDbContext
	{
		protected JsonRepositoryBase(IAmbientDbContextConfigurator ambientDbContextConfigurator) : base(
			ambientDbContextConfigurator)
		{
		}

		public async Task<ICollection<T>> All()
		{
			return await GetDbSet().AsNoTracking().Select(a => a.ToDomain()).ToListAsync();
		}

		public async Task<T> Fetch(Reference<T> aggregate)
		{
			return (await GetDbSet().AsNoTracking().Where(a => a.Id == aggregate.Id).SingleAsync()).ToDomain();
		}

		public virtual async Task Save(T aggregate)
		{
			var entity = await GetEntityWithTracking(aggregate);
			entity.SetValuesFrom(aggregate);
		}

		public async Task SaveAll(ICollection<T> aggregates)
		{
			foreach (var aggregate in aggregates)
			{
				await Save(aggregate);
			}
		}

		public virtual async Task Remove(T aggregate)
		{
			var entity = await GetDbSet().SingleOrDefaultAsync(a => a.Id == aggregate.Id);
			if (entity != null)
			{
				RemoveEntity(entity);
			}
		}

		public async Task RemoveAll(ICollection<T> aggregates)
		{
			foreach (var aggregate in aggregates)
			{
				await Remove(aggregate);
			}
		}

		protected abstract TU AddEntity();

		protected abstract DbSet<TU> GetDbSet();

		protected virtual async Task<TU> GetEntityWithTracking(T aggregate)
		{
			return await GetDbSet().SingleOrDefaultAsync(a => a.Id == aggregate.Id) ?? AddEntity();
		}

		protected abstract void RemoveEntity(TU entity);
	}
}