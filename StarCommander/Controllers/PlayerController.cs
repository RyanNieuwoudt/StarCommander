using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarCommander.Application;
using StarCommander.Application.Services;
using StarCommander.Domain.Players;
using StarCommander.Shared.Model;

namespace StarCommander.Controllers
{
	[Authorize]
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class PlayerController : ControllerBase
	{
		readonly ICommandService commandService;
		readonly IPlayerService playerService;

		public PlayerController(ICommandService commandService, IPlayerService playerService)
		{
			this.commandService = commandService;
			this.playerService = playerService;
		}

		[AllowAnonymous]
		[HttpPost("session")]
		public async Task<IActionResult> SignIn([FromBody] SignIn signIn)
		{
			return Ok(await playerService.SignIn(signIn.CallSign, signIn.Password));
		}

		[AllowAnonymous]
		[HttpPost("player")]
		public async Task<IActionResult> SignUp([FromBody] SignUp signUp)
		{
			return Ok(await playerService.SignUp(signUp.CallSign, signUp.FirstName, signUp.LastName, signUp.Password));
		}

		[HttpPost("name")]
		public async Task<IActionResult> UpdateName([FromBody] PlayerName player)
		{
			await commandService.Issue(new UpdatePlayerName(User.CallSign(), player.FirstName, player.LastName));
			return Ok();
		}
	}
}