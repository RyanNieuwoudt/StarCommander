using AmbientDbContextConfigurator;
using Microsoft.EntityFrameworkCore;
using StarCommander.Infrastructure.Persistence.Aggregate.Messages;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Ships;

public class ShipDataContext : ConfiguringDbContext
{
	public ShipDataContext()
	{
	}

	public ShipDataContext(DbContextOptions<ShipDataContext> options) : base(options)
	{
	}

	public DbSet<Command> ShipCommands { get; set; } = default!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);

		modelBuilder.Entity<Command>().HasKey(r => r.Id);

		modelBuilder.Entity<Command>().Property(r => r.Created).IsRequired();
		modelBuilder.Entity<Command>().HasIndex(r => r.Created);

		modelBuilder.Entity<Command>().Property(r => r.Json).IsRequired();

		modelBuilder.Entity<Command>().Property(r => r.Processed);
		modelBuilder.Entity<Command>().HasIndex(r => r.Processed);
	}
}