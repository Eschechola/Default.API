using System;

namespace Default.Domain.DTOs.Entities;

public abstract record EntityDto
{
    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}