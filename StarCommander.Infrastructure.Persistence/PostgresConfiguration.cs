using System;
using AmbientDbContextConfigurator;
using Microsoft.EntityFrameworkCore;

namespace StarCommander.Infrastructure.Persistence
{
	public sealed class PostgresConfiguration : IDbContextConfiguration
	{
		readonly string connectionString;

		public PostgresConfiguration(string connectionString)
		{
			this.connectionString = connectionString;
		}

		public Action<DbContextOptionsBuilder> Configure => builder => builder.UseNpgsql(connectionString);
	}
}