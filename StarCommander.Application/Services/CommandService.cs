using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using StarCommander.Domain;
using StarCommander.Domain.Messages;
using StarCommander.Domain.Players;

namespace StarCommander.Application.Services
{
	public class CommandService : ICommandService
	{
		readonly IDbContextScopeFactory dbContextScopeFactory;
		readonly IReferenceGenerator generator;
		readonly IPlayerCommandRepository playerCommandRepository;
		readonly IPlayerRepository playerRepository;

		public CommandService(IDbContextScopeFactory dbContextScopeFactory, IReferenceGenerator generator,
			IPlayerCommandRepository playerCommandRepository, IPlayerRepository playerRepository)
		{
			this.dbContextScopeFactory = dbContextScopeFactory;
			this.generator = generator;
			this.playerCommandRepository = playerCommandRepository;
			this.playerRepository = playerRepository;
		}

		public async Task Issue(PlayerCommand command)
		{
			using var dbContextScope = dbContextScopeFactory.Create();

			var player = await playerRepository.Fetch(command.CallSign);

			await playerCommandRepository.Save(Command<Player>.Wrap(generator.NewReference<Message<ICommand>>(),
				player.Reference, command));

			await dbContextScope.SaveChangesAsync();
		}
	}
}