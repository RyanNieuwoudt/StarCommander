using System.Security.Claims;

namespace StarCommander.Application
{
	public static class ClaimsPrincipleExtensions
	{
		public static string CallSign(this ClaimsPrincipal principal)
		{
			return principal?.Identity?.Name ?? string.Empty;
		}
	}
}