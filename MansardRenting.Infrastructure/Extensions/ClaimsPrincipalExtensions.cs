using System.Security.Claims;
namespace MansardRenting.Web.Infrastructure.Extensions
{
	public static class ClaimsPrincipalExtensions
	{
		public static string? GetId(this ClaimsPrincipal userClaimsPrincipal)
		{
			return userClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
		}
	}
}
