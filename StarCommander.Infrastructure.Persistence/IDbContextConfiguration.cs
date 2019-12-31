using System;
using Microsoft.EntityFrameworkCore;

namespace StarCommander.Infrastructure.Persistence
{
	public interface IDbContextConfiguration
	{
		public Action<DbContextOptionsBuilder> Configure { get; }
	}
}