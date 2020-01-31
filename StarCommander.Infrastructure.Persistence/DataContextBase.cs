using EntityFramework.DbContextScope.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace StarCommander.Infrastructure.Persistence
{
	public abstract class DataContextBase : DbContext, IDbContext
	{
		protected DataContextBase()
		{
		}

		protected DataContextBase(DbContextOptions options) : base(options)
		{
		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				AmbientDbContextConfigurator.Current?.Configure(optionsBuilder);
			}
		}
	}
}