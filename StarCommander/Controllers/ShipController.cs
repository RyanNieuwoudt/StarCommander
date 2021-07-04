using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StarCommander.Application.Queries;
using StarCommander.Application.Services;
using StarCommander.Domain;
using StarCommander.Domain.Ships;

namespace StarCommander.Controllers
{
	[Authorize]
	[Produces("application/json")]
	[Route("api/[controller]/{shipId:guid}")]
	public class ShipController : ControllerBase
	{
		readonly ICommandService commandService;
		readonly IShipQuery shipQuery;

		public ShipController(ICommandService commandService, IShipQuery shipQuery)
		{
			this.commandService = commandService;
			this.shipQuery = shipQuery;
		}

		[HttpGet("scan")]
		public async Task<IActionResult> Scan(Guid shipId)
		{
			return Ok(await shipQuery.ScanForNearbyShips(new Reference<Ship>(shipId)));
		}

		[HttpPost("heading/{heading:int}")]
		public async Task<IActionResult> SetHeading(Guid shipId, int heading)
		{
			await commandService.Issue(new SetHeading(new Reference<Ship>(shipId), new Heading(heading)));
			return Ok();
		}

		[HttpPost("speed/{speed:long}")]
		public async Task<IActionResult> SetSpeed(Guid shipId, long speed)
		{
			await commandService.Issue(new SetSpeed(new Reference<Ship>(shipId), new Speed(speed)));
			return Ok();
		}
	}
}