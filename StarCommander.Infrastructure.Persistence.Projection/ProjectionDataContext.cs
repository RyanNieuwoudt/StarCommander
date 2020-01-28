using Microsoft.EntityFrameworkCore;
using StarCommander.Infrastructure.Persistence.Projection.ShipPositions;

namespace StarCommander.Infrastructure.Persistence.Projection
{
	public sealed class ProjectionDataContext : DataContextBase
	{
		public ProjectionDataContext()
		{
		}

		public ProjectionDataContext(DbContextOptions<ProjectionDataContext> options) : base(options)
		{
		}

		public DbSet<ShipLocation> ShipPositions { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			BuildShipPosition(modelBuilder);
		}

		static void BuildShipPosition(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ShipLocation>()
				.Property(a => a.ShipPositionId)
				.IsRequired()
				.ValueGeneratedOnAdd();

			modelBuilder.Entity<ShipLocation>()
				.HasKey(a => a.ShipPositionId);

			modelBuilder.Entity<ShipLocation>().Property(a => a.Heading).IsRequired();

			modelBuilder.Entity<ShipLocation>().Property(a => a.Speed).IsRequired();

			modelBuilder.Entity<ShipLocation>().Property(a => a.X).IsRequired();
			modelBuilder.Entity<ShipLocation>().HasIndex(a => a.X);

			modelBuilder.Entity<ShipLocation>().Property(a => a.Y).IsRequired();
			modelBuilder.Entity<ShipLocation>().HasIndex(a => a.Y);

			modelBuilder.Entity<ShipLocation>().Property(a => a.Created).IsRequired();
		}
	}
}