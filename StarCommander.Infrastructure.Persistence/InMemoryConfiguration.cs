using System;
using AmbientDbContextConfigurator;
using Microsoft.EntityFrameworkCore;

namespace StarCommander.Infrastructure.Persistence;

public class InMemoryConfiguration : IDbContextConfiguration
{
	readonly string databaseName;

	public InMemoryConfiguration(string databaseName)
	{
		this.databaseName = databaseName;
	}

	public Action<DbContextOptionsBuilder> Configure => builder => builder.UseInMemoryDatabase(databaseName);
}