using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Otanimes.Domain.DTOs.Authorization;

namespace Otanimes.API.Authorization.Services;

public class TokenService(IConfiguration configuration) : ITokenService
{
    private readonly string issuer = configuration["Jwt:Issuer"];
    private readonly string audience = configuration["Jwt:Audience"];
    private readonly string secret = configuration["Jwt:Secret"];
    private readonly DateTime accessTokenExpiresAt = DateTime.UtcNow.AddMinutes(int.Parse(configuration["Jwt:AccessTokenExpirationMinutes"]));
    
    public TokenDto CreateToken(Guid id, string username)
    {
        var accessToken = CreateAccessToken(id, username);

        return new TokenDto
        {
            Issuer = issuer,
            IssuedAt = DateTime.UtcNow,
            AccessToken = accessToken,
            AccessTokenExpiresAt = accessTokenExpiresAt
        };
    }
    
    private string CreateAccessToken(Guid id, string username)
    {
        var handler = new JwtSecurityTokenHandler();
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = GetClaims(id, username),
            Expires = accessTokenExpiresAt,
            SigningCredentials = credentials,
            Issuer = issuer,
            Audience = audience
        };

        var token = handler.CreateToken(tokenDescriptor);
        return handler.WriteToken(token);
    }
    
    private static ClaimsIdentity GetClaims(Guid id, string username)
    {
        var claims = new List<Claim>
        {
            new ("Id", id.ToString()),
            new (ClaimTypes.Role, "User"),
            new (ClaimTypes.NameIdentifier, username),
            new (JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new (JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString("yyyy/MM/dd"))
        };
        
        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaims(claims);

        return claimsIdentity;
    }
}