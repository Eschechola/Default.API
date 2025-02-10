using System;

namespace Otanimes.Domain.DTOs.Entities;

public record UserDto : EntityDto
{
    public string Username { get; set; }
    public DateTime? LastLoginDate { get; set; }
}