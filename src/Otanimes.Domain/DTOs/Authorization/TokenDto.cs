using System;

namespace Otanimes.Domain.DTOs.Authorization;

public record TokenDto
{
    public const string Alg = "SHA512";
    public const string Type = "JWT";
    public string Issuer { get; init; }
    public string AccessToken { get; init; }
    public DateTime IssuedAt { get; init; }
    public DateTime AccessTokenExpiresAt { get; init; }
}