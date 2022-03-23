using AmbientDbContextConfigurator;
using EntityFramework.DbContextScope.Interfaces;

namespace StarCommander.Infrastructure.Persistence;

public abstract class RepositoryBase<T> where T : class, IDbContext
{
	readonly IAmbientDbContextConfigurator ambientDbContextConfigurator;

	protected RepositoryBase(IAmbientDbContextConfigurator ambientDbContextConfigurator)
	{
		this.ambientDbContextConfigurator = ambientDbContextConfigurator;
	}

	protected T DataContext => ambientDbContextConfigurator.Get<T>();
}