using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using StarCommander.Domain;
using StarCommander.Domain.Messages;
using StarCommander.Domain.Players;
using StarCommander.Domain.Ships;
using static StarCommander.Domain.Messages.Command;

namespace StarCommander.Application.Services
{
	public class CommandService : ICommandService
	{
		readonly IDbContextScopeFactory dbContextScopeFactory;
		readonly IReferenceGenerator generator;
		readonly IPlayerCommandRepository playerCommandRepository;
		readonly IShipCommandRepository shipCommandRepository;

		public CommandService(IDbContextScopeFactory dbContextScopeFactory, IReferenceGenerator generator,
			IPlayerCommandRepository playerCommandRepository, IShipCommandRepository shipCommandRepository)
		{
			this.dbContextScopeFactory = dbContextScopeFactory;
			this.generator = generator;
			this.playerCommandRepository = playerCommandRepository;
			this.shipCommandRepository = shipCommandRepository;
		}

		public async Task Issue(PlayerCommand command)
		{
			using var dbContextScope = dbContextScopeFactory.Create();

			await playerCommandRepository.Save(Wrap(generator.NewReference<Message<ICommand>>(), command.Player,
				command));

			await dbContextScope.SaveChangesAsync();
		}

		public async Task Issue(ShipCommand command)
		{
			using var dbContextScope = dbContextScopeFactory.Create();

			await shipCommandRepository.Save(Wrap(generator.NewReference<Message<ICommand>>(), command.Ship, command));

			await dbContextScope.SaveChangesAsync();
		}
	}
}