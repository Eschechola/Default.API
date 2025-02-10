using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Otanimes.API.Authorization.Extensions;

public static class IdentityExtensions
{
    public static string GetId(this ClaimsPrincipal principal)
        => principal.FindFirst("Id")?.Value;

    public static string GetRole(this ClaimsPrincipal principal)
        => principal.FindFirst(ClaimTypes.Role)?.Value;
    
    public static string GeTokenIdentifier(this ClaimsPrincipal principal)
        => principal.FindFirst(JwtRegisteredClaimNames.Jti)?.Value;
    
    public static string GetTokenIssuedAt(this ClaimsPrincipal principal)
        => principal.FindFirst(JwtRegisteredClaimNames.Iat)?.Value;
}