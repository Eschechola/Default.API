using Otanimes.Domain.DTOs.Entities;

namespace Otanimes.Domain.DTOs.Authorization;

public record LoginDto
{
    public UserDto User { get; set; }
    public TokenDto Token { get; set; }
}