using System;
using System.Security;
using System.Threading.Tasks;
using AutoMapper;
using EntityFramework.DbContextScope.Interfaces;
using StarCommander.Domain;
using StarCommander.Domain.Players;
using StarCommander.Shared.Model;
using Player = StarCommander.Domain.Players.Player;

namespace StarCommander.Application.Services
{
	public class PlayerService : IPlayerService
	{
		readonly IDbContextScopeFactory dbContextScopeFactory;
		readonly IMapper mapper;
		readonly IPlayerRepository playerRepository;

		public PlayerService(IDbContextScopeFactory dbContextScopeFactory, IMapper mapper,
			IPlayerRepository playerRepository)
		{
			this.dbContextScopeFactory = dbContextScopeFactory;
			this.mapper = mapper;
			this.playerRepository = playerRepository;
		}

		public async Task<Session> SignIn(string callSign, string password)
		{
			try
			{
				using var dbContextScope = dbContextScopeFactory.Create();

				var player = await playerRepository.Fetch(callSign);

				if (!Password.VerifyPasswordHash(password, player.PasswordHash, player.PasswordSalt))
				{
					throw new Exception();
				}

				//TODO Mapper
				return new Session
				{
					Token = Guid.NewGuid().ToString(),
					Player = mapper.Map<Shared.Model.Player>(player)
				};
			}
			catch
			{
				throw new SecurityException("Invalid call sign or password.");
			}
		}

		public async Task<Session> SignUp(string callSign, string firstName, string lastName, string password)
		{
			//TODO Validation

			using var dbContextScope = dbContextScopeFactory.Create();

			if (await playerRepository.Exists(callSign))
			{
				throw new InvalidOperationException();
			}

			var (passwordHash, passwordSalt) = Password.CreatePasswordHashWithSalt(password);

			//TODO Service to generate ID
			var player = Player.SignUp(new Reference<Player>(Guid.NewGuid()), callSign, firstName, lastName,
				passwordHash, passwordSalt);

			await playerRepository.Save(player);

			await dbContextScope.SaveChangesAsync();

			//TODO Mapper
			return new Session
			{
				Token = Guid.NewGuid().ToString(),
				Player = mapper.Map<Shared.Model.Player>(player)
			};
		}
	}
}