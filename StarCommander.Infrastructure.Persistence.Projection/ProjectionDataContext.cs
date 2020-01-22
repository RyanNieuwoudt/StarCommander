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

		public DbSet<ShipPosition> ShipPositions { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			BuildShipPosition(modelBuilder);
		}

		static void BuildShipPosition(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<ShipPosition>()
				.Property(a => a.ShipPositionId)
				.IsRequired()
				.ValueGeneratedOnAdd();

			modelBuilder.Entity<ShipPosition>()
				.HasKey(a => a.ShipPositionId);

			modelBuilder.Entity<ShipPosition>().Property(a => a.X).IsRequired();
			modelBuilder.Entity<ShipPosition>().HasIndex(a => a.X);

			modelBuilder.Entity<ShipPosition>().Property(a => a.Y).IsRequired();
			modelBuilder.Entity<ShipPosition>().HasIndex(a => a.Y);

			modelBuilder.Entity<ShipPosition>().Property(a => a.Created).IsRequired();
		}
	}
}