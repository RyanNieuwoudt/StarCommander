using System;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using StarCommander.Domain;
using StarCommander.Domain.Messages;
using static StarCommander.Domain.Messages.Command;

namespace StarCommander.Application.Services
{
	public class CommandService : ICommandService
	{
		readonly IDbContextScopeFactory dbContextScopeFactory;
		readonly IReferenceGenerator generator;
		readonly ICommandRepository commandRepository;

		public CommandService(ICommandRepository commandRepository, IDbContextScopeFactory dbContextScopeFactory,
			IReferenceGenerator generator)
		{
			this.commandRepository = commandRepository;
			this.dbContextScopeFactory = dbContextScopeFactory;
			this.generator = generator;
		}

		public async Task Issue(ICommand command, DateTimeOffset? scheduledFor = null)
		{
			using var dbContextScope = dbContextScopeFactory.Create();
			await commandRepository.Save(Wrap(generator.NewReference<Message<ICommand>>(), command, scheduledFor));
			await dbContextScope.SaveChangesAsync();
		}
	}
}