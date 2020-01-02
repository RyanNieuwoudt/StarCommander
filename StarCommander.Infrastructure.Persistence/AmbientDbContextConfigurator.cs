using System;
using EntityFramework.DbContextScope.Interfaces;

namespace StarCommander.Infrastructure.Persistence
{
	public class AmbientDbContextConfigurator : IAmbientDbContextConfigurator
	{
		[ThreadStatic]
		public static IDbContextConfiguration? Current;

		readonly IAmbientDbContextLocator ambientDbContextLocator;
		readonly IDbContextConfiguration configuration;

		public AmbientDbContextConfigurator(IAmbientDbContextLocator ambientDbContextLocator,
			IDbContextConfiguration configuration)
		{
			this.ambientDbContextLocator = ambientDbContextLocator;
			this.configuration = configuration;
		}

		public T Get<T>() where T : class, IDbContext
		{
			Current = configuration;
			return ambientDbContextLocator.Get<T>();
		}
	}
}