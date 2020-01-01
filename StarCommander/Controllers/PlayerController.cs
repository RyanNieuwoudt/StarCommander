using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarCommander.Application.Services;
using StarCommander.Shared.Model;

namespace StarCommander.Controllers
{
	[Authorize]
	[Produces("application/json")]
	[Route("api/[controller]")]
	public class PlayerController : ControllerBase
	{
		readonly IPlayerService playerService;

		public PlayerController(IPlayerService playerService)
		{
			this.playerService = playerService;
		}

		[AllowAnonymous]
		[HttpPost("player")]
		public async Task<IActionResult> SignUp([FromBody] Player player)
		{
			return Ok(await playerService.SignUp(player.CallSign, player.FirstName, player.LastName));
		}
	}
}