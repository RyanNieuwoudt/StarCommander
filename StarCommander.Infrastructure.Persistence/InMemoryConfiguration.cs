using System;
using Microsoft.EntityFrameworkCore;

namespace StarCommander.Infrastructure.Persistence
{
	public sealed class InMemoryConfiguration : IDbContextConfiguration
	{
		readonly string databaseName;

		public InMemoryConfiguration(string databaseName)
		{
			this.databaseName = databaseName;
		}

		public Action<DbContextOptionsBuilder> Configure => builder => builder.UseInMemoryDatabase(databaseName);
	}
}