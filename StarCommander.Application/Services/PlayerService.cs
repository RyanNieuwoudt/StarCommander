using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EntityFramework.DbContextScope.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StarCommander.Domain;
using StarCommander.Domain.Players;
using StarCommander.Shared.Model;
using Player = StarCommander.Domain.Players.Player;

namespace StarCommander.Application.Services
{
	public class PlayerService : IPlayerService
	{
		readonly AppSettings appSettings;
		readonly IDbContextScopeFactory dbContextScopeFactory;
		readonly IReferenceGenerator generator;
		readonly IMapper mapper;
		readonly IPlayerRepository playerRepository;

		public PlayerService(IOptions<AppSettings> appSettings, IDbContextScopeFactory dbContextScopeFactory,
			IReferenceGenerator generator, IMapper mapper, IPlayerRepository playerRepository)
		{
			this.appSettings = appSettings.Value;
			this.dbContextScopeFactory = dbContextScopeFactory;
			this.generator = generator;
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

				return GetSession(player);
			}
			catch
			{
				throw new SecurityException("Invalid call sign or password.");
			}
		}

		public async Task<Session> SignUp(string callSign, string firstName, string lastName, string password)
		{
			//TODO Validation
			try
			{
				using var dbContextScope = dbContextScopeFactory.Create();

				if (await playerRepository.Exists(callSign))
				{
					throw new Exception();
				}

				var (passwordHash, passwordSalt) = Password.CreatePasswordHashWithSalt(password);

				var player = Player.SignUp(generator.NewReference<Player>(), callSign, firstName, lastName,
					passwordHash, passwordSalt);

				await playerRepository.Save(player);
				await dbContextScope.SaveChangesAsync();

				return GetSession(player);
			}
			catch
			{
				throw new InvalidOperationException("Call sign in use.");
			}
		}

		public async Task UpdateName(Reference<Player> id, string firstName, string lastName)
		{
			using var dbContextScope = dbContextScopeFactory.Create();

			var player = await playerRepository.Fetch(id);

			player.UpdateName(firstName, lastName);

			await playerRepository.Save(player);
			await dbContextScope.SaveChangesAsync();
		}

		Session GetSession(Player player)
		{
			return new Session { Token = GetToken(player), Player = mapper.Map<Shared.Model.Player>(player) };
		}

		string GetToken(Player player)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(appSettings.Secret);
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(GetClaims(player)),
				Expires = DateTime.UtcNow.AddDays(2),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
					SecurityAlgorithms.HmacSha256Signature)
			};
			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		static IEnumerable<Claim> GetClaims(Player player)
		{
			yield return new Claim(ClaimTypes.Name, player.Id.ToString());
			yield return new Claim(ClaimTypes.NameIdentifier, player.CallSign);
		}
	}
}