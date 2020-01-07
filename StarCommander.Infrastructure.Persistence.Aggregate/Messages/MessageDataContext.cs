using Microsoft.EntityFrameworkCore;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Messages
{
	public class MessageDataContext : DataContextBase
	{
		public MessageDataContext()
		{
		}

		public MessageDataContext(DbContextOptions<MessageDataContext> options) : base(options)
		{
		}

		public DbSet<Event> Events { get; set; } = default!;
		public DbSet<Job> Jobs { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			BuildEvent(modelBuilder);
			BuildJob(modelBuilder);
		}

		static void BuildEvent(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Event>().HasKey(r => r.Id);

			modelBuilder.Entity<Event>().Property(r => r.Created).IsRequired();
			modelBuilder.Entity<Event>().HasIndex(r => r.Created);

			modelBuilder.Entity<Event>().Property(r => r.Json).IsRequired();

			modelBuilder.Entity<Event>().Property(r => r.Processed);
			modelBuilder.Entity<Event>().HasIndex(r => r.Processed);
		}

		static void BuildJob(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Job>().HasKey(j => j.Id);

			modelBuilder.Entity<Job>().Property(j => j.QueueId).IsRequired();
			modelBuilder.Entity<Job>().HasIndex(j => j.QueueId);

			modelBuilder.Entity<Job>().Property(j => j.MessageId).IsRequired();
			modelBuilder.Entity<Job>().HasIndex(j => j.MessageId);

			modelBuilder.Entity<Job>().Property(j => j.Handler).IsRequired();

			modelBuilder.Entity<Job>().Property(j => j.Created).IsRequired();
		}
	}
}