using System.Data.Common;
using EntityFramework.DbContextScope.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace StarCommander.Infrastructure.Persistence.Projection
{
	public abstract class RepositoryBase<T> : Persistence.RepositoryBase<T> where T : class, IDbContext
	{
		protected RepositoryBase(IAmbientDbContextConfigurator ambientDbContextConfigurator) : base(
			ambientDbContextConfigurator)
		{
		}

		protected DbConnection Connection => DataContext.Database.GetDbConnection();
	}
}