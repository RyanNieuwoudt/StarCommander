using System;
using System.Security.Claims;
using StarCommander.Domain;
using StarCommander.Domain.Players;
using static StarCommander.Domain.Reference;

namespace StarCommander.Application;

public static class ClaimsPrincipleExtensions
{
	public static Reference<Player> Id(this ClaimsPrincipal principal)
	{
		return To<Player>(Guid.Parse(principal.Identity!.Name!));
	}

	public static string CallSign(this ClaimsPrincipal principal)
	{
		return principal.Identity?.Name ?? string.Empty;
	}
}