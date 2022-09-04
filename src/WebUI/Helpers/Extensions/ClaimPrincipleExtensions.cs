using System.Security.Claims;

namespace WebUI.Helpers.Extensions
{
    public static class ClaimPrincipleExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var value = user.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return int.Parse(value);
        }
    }
}
