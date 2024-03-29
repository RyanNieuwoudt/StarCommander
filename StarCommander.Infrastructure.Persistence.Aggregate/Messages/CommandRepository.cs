using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AmbientDbContextConfigurator;
using Microsoft.EntityFrameworkCore;
using NodaTime;
using StarCommander.Domain.Messages;

namespace StarCommander.Infrastructure.Persistence.Aggregate.Messages;

public class CommandRepository : JsonRepositoryBase<Domain.Messages.Command, Command, MessageDataContext>,
	ICommandRepository
{
	readonly IClock clock;

	public CommandRepository(IAmbientDbContextConfigurator ambientDbContextConfigurator, IClock clock)
		: base(ambientDbContextConfigurator)
	{
		this.clock = clock;
	}

	public async Task<Domain.Messages.Command?> FetchNextUnprocessed()
	{
		var now = clock.GetCurrentInstant();

		return (await GetDbSet()
			.AsNoTracking()
			.Where(e => e.Processed == null)
			.Where(e => e.ScheduledFor == null || e.ScheduledFor <= now)
			.OrderBy(e => e.Created)
			.FirstOrDefaultAsync())?.ToDomain();
	}

	public async Task<IEnumerable<Domain.Messages.Command>> FetchForTarget(Guid targetId) => await GetDbSet()
		.AsNoTracking()
		.Where(c => c.TargetId == targetId)
		.OrderBy(c => c.Created)
		.Select(c => c.ToDomain())
		.ToListAsync();

	public async Task<IEnumerable<Domain.Messages.Command>> FetchScheduledForTarget(Guid targetId)
	{
		var now = clock.GetCurrentInstant();

		return await GetDbSet()
			.AsNoTracking()
			.Where(c => c.TargetId == targetId)
			.Where(e => e.Processed == null)
			.Where(e => e.ScheduledFor > now)
			.OrderBy(c => c.Created)
			.Select(c => c.ToDomain())
			.ToListAsync();
	}

	protected override Command AddEntity()
	{
		var entity = new Command();
		DataContext.Add(entity);
		return entity;
	}

	protected override DbSet<Command> GetDbSet() => DataContext.Commands;

	protected override void RemoveEntity(Command entity) => DataContext.Remove(entity);
}