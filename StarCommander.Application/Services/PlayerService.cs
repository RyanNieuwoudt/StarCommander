using System;
using System.Threading.Tasks;
using EntityFramework.DbContextScope.Interfaces;
using StarCommander.Domain;
using StarCommander.Domain.Players;
using StarCommander.Shared.Model;
using Player = StarCommander.Domain.Players.Player;

namespace StarCommander.Application.Services
{
	public sealed class PlayerService : IPlayerService
	{
		readonly IDbContextScopeFactory dbContextScopeFactory;
		readonly IPlayerRepository playerRepository;

		public PlayerService(IDbContextScopeFactory dbContextScopeFactory, IPlayerRepository playerRepository)
		{
			this.dbContextScopeFactory = dbContextScopeFactory;
			this.playerRepository = playerRepository;
		}

		public async Task<Session> SignUp(string callSign, string firstName, string lastName)
		{
			//TODO Validation

			using var dbContextScope = dbContextScopeFactory.Create();

			//TODO Service to generate ID
			var player = Player.Create(new Reference<Player>(Guid.NewGuid()), callSign, firstName, lastName);

			//TODO Do not overwrite existing player
			await playerRepository.Save(player);

			await dbContextScope.SaveChangesAsync();

			//TODO Mapper
			return new Session
			{
				Token = Guid.NewGuid().ToString(),
				Player = new Shared.Model.Player { CallSign = callSign, FirstName = firstName, LastName = lastName }
			};
		}
	}
}