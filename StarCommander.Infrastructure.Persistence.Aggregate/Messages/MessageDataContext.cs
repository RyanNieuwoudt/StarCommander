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

		public DbSet<Command> Commands { get; set; } = default!;
		public DbSet<Event> Events { get; set; } = default!;
		public DbSet<Job> Jobs { get; set; } = default!;

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			BuildCommand(modelBuilder);
			BuildEvent(modelBuilder);
			BuildJob(modelBuilder);
		}

		static void BuildCommand(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Command>().HasKey(c => c.Id);

			modelBuilder.Entity<Command>().Property(c => c.Created).IsRequired();
			modelBuilder.Entity<Command>().HasIndex(c => c.Created);

			modelBuilder.Entity<Command>().Property(c => c.Json).IsRequired();

			modelBuilder.Entity<Command>().Property(c => c.Processed);
			modelBuilder.Entity<Command>().HasIndex(c => c.Processed);

			modelBuilder.Entity<Command>().Property(c => c.TargetId);
			modelBuilder.Entity<Command>().HasIndex(c => c.TargetId);
		}

		static void BuildEvent(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Event>().HasKey(e => e.Id);

			modelBuilder.Entity<Event>().Property(e => e.Created).IsRequired();
			modelBuilder.Entity<Event>().HasIndex(e => e.Created);

			modelBuilder.Entity<Event>().Property(e => e.Json).IsRequired();

			modelBuilder.Entity<Event>().Property(e => e.Processed);
			modelBuilder.Entity<Event>().HasIndex(e => e.Processed);
		}

		static void BuildJob(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Job>().HasKey(j => j.Id);

			modelBuilder.Entity<Job>().Property(j => j.QueueId).IsRequired();
			modelBuilder.Entity<Job>().HasIndex(j => j.QueueId);

			modelBuilder.Entity<Job>().Property(j => j.MessageId).IsRequired();
			modelBuilder.Entity<Job>().HasIndex(j => j.MessageId);

			modelBuilder.Entity<Job>().Property(j => j.Address).IsRequired();

			modelBuilder.Entity<Job>().Property(j => j.Handler).IsRequired();

			modelBuilder.Entity<Job>().Property(j => j.Created).IsRequired();
		}
	}
}