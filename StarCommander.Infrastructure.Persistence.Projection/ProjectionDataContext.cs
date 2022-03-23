using AmbientDbContextConfigurator;
using Microsoft.EntityFrameworkCore;
using StarCommander.Infrastructure.Persistence.Projection.ShipLocations;

namespace StarCommander.Infrastructure.Persistence.Projection;

public sealed class ProjectionDataContext : ConfiguringDbContext
{
	public ProjectionDataContext()
	{
	}

	public ProjectionDataContext(DbContextOptions<ProjectionDataContext> options) : base(options)
	{
	}

	public DbSet<ShipLocation> ShipLocations { get; set; } = default!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		BuildShipLocation(modelBuilder);
	}

	static void BuildShipLocation(ModelBuilder modelBuilder)
	{
		modelBuilder.Entity<ShipLocation>().Property(a => a.ShipLocationId).IsRequired().ValueGeneratedOnAdd();

		modelBuilder.Entity<ShipLocation>().HasKey(a => a.ShipLocationId);

		modelBuilder.Entity<ShipLocation>().Property(a => a.Heading).IsRequired();

		modelBuilder.Entity<ShipLocation>().Property(a => a.Speed).IsRequired();

		modelBuilder.Entity<ShipLocation>().Property(a => a.X).IsRequired();
		modelBuilder.Entity<ShipLocation>().HasIndex(a => a.X);

		modelBuilder.Entity<ShipLocation>().Property(a => a.Y).IsRequired();
		modelBuilder.Entity<ShipLocation>().HasIndex(a => a.Y);

		modelBuilder.Entity<ShipLocation>().Property(a => a.Created).IsRequired();
	}
}