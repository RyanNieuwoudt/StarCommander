using AmbientDbContextConfigurator;
using Microsoft.EntityFrameworkCore;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Players
{
	public class PlayerDataContext : ConfiguringDbContext
	{
		public PlayerDataContext()
		{
		}

		public PlayerDataContext(DbContextOptions<PlayerDataContext> options) : base(options)
		{
		}

		public DbSet<Player> Players { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Player>().HasKey(p => p.Id);

			modelBuilder.Entity<Player>().Property(p => p.CallSign).IsRequired();
			modelBuilder.Entity<Player>().HasIndex(p => p.CallSign).IsUnique();

			modelBuilder.Entity<Player>().Property(p => p.Json).IsRequired();
		}
	}
}