using Microsoft.EntityFrameworkCore;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Messages
{
	public class EventDataContext : DataContextBase
	{
		public EventDataContext()
		{
		}

		public EventDataContext(DbContextOptions<EventDataContext> options) : base(options)
		{
		}

		public DbSet<Event> Events { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Event>().HasKey(r => r.Id);

			modelBuilder.Entity<Event>().Property(r => r.Created).IsRequired();
			modelBuilder.Entity<Event>().HasIndex(r => r.Created);

			modelBuilder.Entity<Event>().Property(r => r.Json).IsRequired();

			modelBuilder.Entity<Event>().Property(r => r.Processed);
			modelBuilder.Entity<Event>().HasIndex(r => r.Processed);
		}
	}
}