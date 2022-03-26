using System;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using NodaTime;
using StarCommander.Domain;
using StarCommander.Domain.Messages;
using static StarCommander.Domain.Messages.Command;

namespace StarCommander.Application.Services;

public class CommandService : ICommandService
{
	readonly IClock clock;
	readonly ICommandRepository commandRepository;
	readonly IDbContextScopeFactory dbContextScopeFactory;
	readonly IReferenceGenerator generator;

	public CommandService(IClock clock, ICommandRepository commandRepository,
		IDbContextScopeFactory dbContextScopeFactory, IReferenceGenerator generator)
	{
		this.clock = clock;
		this.commandRepository = commandRepository;
		this.dbContextScopeFactory = dbContextScopeFactory;
		this.generator = generator;
	}

	public async Task Issue(ICommand command, Instant? scheduledFor = null)
	{
		using var dbContextScope = dbContextScopeFactory.Create();
		await commandRepository.Save(Wrap(generator.NewReference<Message<ICommand>>(), command,
			clock.GetCurrentInstant(), scheduledFor));
		await dbContextScope.SaveChangesAsync();
	}
}