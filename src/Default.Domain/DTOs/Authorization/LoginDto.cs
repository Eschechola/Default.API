using Default.Domain.DTOs.Entities;

namespace Default.Domain.DTOs.Authorization;

public record LoginDto
{
    public UserDto User { get; set; }
    public TokenDto Token { get; set; }
}