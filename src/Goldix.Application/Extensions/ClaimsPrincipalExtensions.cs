namespace Goldix.Application.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetCurrentUserId(this ClaimsPrincipal user)
    {
        var userIdClaim = user.FindFirstValue(ClaimTypes.NameIdentifier);
        return userIdClaim;
    }
}
